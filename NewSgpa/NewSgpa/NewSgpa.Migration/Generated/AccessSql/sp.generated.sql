-- Auto-generated from sp.mdb-specs.txt
-- Query count: 155
-- Ordered by dependencies (nested queries first)

-- ===== DATA OBJECT FOR QUERY: 1000_AfiladoCI2Nombre =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1000_AfiladoCI2Nombre_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Afiliado.CI AS CI, [SP_Afiliado].[Nombres] + ' ' + [SP_Afiliado].[Apellido1] + (CASE WHEN [SP_Afiliado].[Apellido2] + ''<>'' THEN ' ' ELSE '' END) + [SP_Afiliado].[Apellido2] AS DescAfiliado, SP_Afiliado.Direccion, SP_Afiliado.Telefono
FROM SP_Afiliado
WHERE (((SP_Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1000_AfiliadoImpLiquidoxMes =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1000_AfiliadoImpLiquidoxMes_q]
AS
SELECT SP_ImpLiquido.CI, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, Sum(SP_ImpLiquido.Importe) AS Importe, MIN(SP_ImpLiquido.AnioMes) AS AnioMes
FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)
WHERE (((SP_Trabaja.FechaBaja) Is Null Or (SP_Trabaja.FechaBaja)>CAST(GETDATE() AS date)) AND ((SP_Trabaja.FechaIngreso)<=CAST(GETDATE() AS date)))
GROUP BY SP_ImpLiquido.CI, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes;
GO

-- ===== DATA OBJECT FOR QUERY: 1000_PrestamoxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1000_PrestamoxIDPrestamo_q](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.*
FROM SP_Prestamo
WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1001_AfiliadoImpLiquidoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1001_AfiliadoImpLiquidoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_ImpLiquido.CI, SP_ImpLiquido.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, SP_ImpLiquido.Importe, SP_ImpLiquido.AnioMes
FROM (SP_Empresa INNER JOIN SP_ImpLiquido ON SP_Empresa.CodEmpresa = SP_ImpLiquido.CodEmpresa) INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CI = SP_Trabaja.CI) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa)
WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null Or (SP_Trabaja.FechaBaja)>CAST(GETDATE() AS date)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1001_FacturaIdMax =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1001_FacturaIdMax_q]
AS
SELECT Max(SP_Factura.IdFactura) AS Max
FROM SP_Factura;
GO

-- ===== DATA OBJECT FOR QUERY: 1001_FacturaMax =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1001_FacturaMax_q]
AS
SELECT Max(SP_Factura.NroFactura) AS Max
FROM SP_Factura;
GO

-- ===== DATA OBJECT FOR QUERY: 1001_PrestamoMax =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1001_PrestamoMax_q]
AS
SELECT Max(SP_Prestamo.IdPrestamo) AS Max
FROM SP_Prestamo;
GO

-- ===== DATA OBJECT FOR QUERY: 1002_CuadroAmortizacionxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_CuadroAmortizacion.*
FROM SP_CuadroAmortizacion
WHERE (((SP_CuadroAmortizacion.IdPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_CuadroAmortizacionxIDPrestamoNroCuota =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamoNroCuota_q](@pIdPrestamo INT, @pNroCuota NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_CuadroAmortizacion.IDPrestamo, SP_CuadroAmortizacion.NroCuota, SP_CuadroAmortizacion.Monto, SP_CuadroAmortizacion.ImporteCuota, SP_CuadroAmortizacion.Interes, SP_CuadroAmortizacion.Amortizacion, SP_CuadroAmortizacion.Saldo, SP_CuadroAmortizacion.Usr, SP_CuadroAmortizacion.Ts
FROM SP_CuadroAmortizacion
WHERE (((SP_CuadroAmortizacion.IDPrestamo)=@pIdPrestamo) AND ((SP_CuadroAmortizacion.NroCuota)=@pNroCuota))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_CuotasxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_CuotasxIDPrestamo_q](@pIdPrestamo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.IdPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_CuotaEstado.Descrip AS DescCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
FROM SP_Cuota INNER JOIN SP_CuotaEstado ON SP_Cuota.CodCuotaEstado = SP_CuotaEstado.CodCuotaEstado
WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_CuotasxIDPrestamoCuotaEstado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_CuotasxIDPrestamoCuotaEstado_q](@pIdPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.*
FROM SP_Cuota
WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_CuotaxIDPrestamoNro =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_CuotaxIDPrestamoNro_q](@pIdPrestamo INT, @pNro NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.*
FROM SP_Cuota
WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo) AND ((SP_Cuota.Nro)=@pNro))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturaDetallexIDFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturaDetallexIDFactura_q](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_FacturaDetalle.*
FROM SP_FacturaDetalle
WHERE (((SP_FacturaDetalle.IdFactura)=@pIdFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturaDetallexIDFacturaCodItemPago =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturaDetallexIDFacturaCodItemPago_q](@pIdFactura INT, @pCodItemPago NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_FacturaDetalle.*
FROM SP_FacturaDetalle
WHERE (((SP_FacturaDetalle.IdFactura)=@pIdFactura) AND ((SP_FacturaDetalle.CodItemPago)=@pCodItemPago))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturasEmitidasXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturasEmitidasXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts
FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)='emi'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturasxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturasxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.CodFacturaTipo
FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturaxIDFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturaxIDFactura_q](@pIDFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.*
FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
WHERE (((SP_Factura.IDFactura)=@pIDFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_FacturaxNroFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_FacturaxNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.Usr, SP_Factura.Ts
FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
WHERE (((SP_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_LiquidoAnioMesxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_LiquidoAnioMesxCI_q](@pCI INT, @pMesIni INT, @pMesFin INT)
RETURNS TABLE
AS
RETURN
(
SELECT DISTINCT SP_ImpLiquido.AnioMes AS AnioMes
FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)
WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null) AND ((SP_ImpLiquido.AnioMes) Between @pMesIni And @pMesFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_PagoItemPagoxIDFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_PagoItemPagoxIDFactura_q](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Pago_ItemPago.*
FROM SP_Pago_ItemPago
WHERE (((SP_Pago_ItemPago.IdFactura)=@pIdFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_PagoxIDFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_PagoxIDFactura_q](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Pago.*
FROM SP_Pago
WHERE (((SP_Pago.IdFactura)=@pIdFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_PrestamoAbiertoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_PrestamoAbiertoxCI_q](@pCI INT, @pCodPrestamoTipo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.*
FROM SP_Prestamo
WHERE (((SP_Prestamo.CI)=@pCI) AND ((SP_Prestamo.CodPrestamoEstado) NOT In ('anu','can','car')) AND ((SP_Prestamo.CodPrestamoTipo)=@pCodPrestamoTipo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_PrestamoxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_PrestamoxIDPrestamo_q](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.*
FROM SP_Prestamo
WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1002_TasasxCodMoneda =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1002_TasasxCodMoneda_q](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Moneda.TasaCambio, SP_Moneda.TasaMora, SP_Moneda.Tasa, SP_Moneda.CodAbitab
FROM SP_Moneda
WHERE (((SP_Moneda.CodMoneda)=@pCodMoneda))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_CuotasPendientes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_CuotasPendientes_q](@pIdPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_CuotaEstado.Descrip AS DescCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
FROM SP_Cuota INNER JOIN SP_CuotaEstado ON SP_Cuota.CodCuotaEstado = SP_CuotaEstado.CodCuotaEstado
WHERE (((SP_Cuota.IDPrestamo)=@pIdPrestamo) AND ((SP_Cuota.FechaVencimiento)<CAST(GETDATE() AS date)) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_CuotaxIDPrestamoNro =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_CuotaxIDPrestamoNro_q](@pIDPrestamo INT, @pNro NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
FROM SP_Cuota
WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.Nro)=@pNro))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_CuotaxNroFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_CuotaxNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.Importe, SP_Cuota.CodCuotaEstado, SP_Cuota.FechaPago, SP_Cuota.IDPrestamo, SP_Cuota.Nro
FROM (SP_FacturaDetalle INNER JOIN SP_Factura ON SP_FacturaDetalle.IdFactura = SP_Factura.IDFactura) INNER JOIN SP_Cuota ON (SP_FacturaDetalle.NroCuota = SP_Cuota.Nro) AND (SP_Factura.IdPrestamo = SP_Cuota.IDPrestamo)
WHERE (((SP_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_FacturasxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_FacturasxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.CodMoneda, SP_Moneda.Descrip AS Moneda, SP_Factura.Importe, SP_Factura.IDFactura, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.FechaPago
FROM (SP_Factura INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_FacturasxIDPrestamoEstado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_FacturasxIDPrestamoEstado_q](@pIDPrestamo INT, @pCodFacturaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.CodFacturaTipo
FROM SP_Factura INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)=@pCodFacturaEstado))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_PagosSingleXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_PagosSingleXIDPrestamo_q](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Pago.*
FROM SP_Pago
WHERE IdFactura in (select IdFactura from SP_Factura where IdPrestamo=@pIdPrestamo)
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_PagosxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_PagosxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Pago.Fecha, SP_Pago.Importe, SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Factura.Usr, SP_Factura.Ts
FROM (SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_PagoxNroFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_PagoxNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Pago.*
FROM SP_Pago INNER JOIN SP_Factura ON SP_Pago.IdFactura = SP_Factura.IdFactura
WHERE (((SP_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1003_SaldoxNroFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1003_SaldoxNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_CuadroAmortizacion.Saldo
FROM (SP_FacturaDetalle INNER JOIN SP_Factura ON SP_FacturaDetalle.IdFactura = SP_Factura.IdFactura) INNER JOIN SP_CuadroAmortizacion ON (SP_Factura.IdPrestamo = SP_CuadroAmortizacion.IdPrestamo) AND (SP_FacturaDetalle.NroCuota = SP_CuadroAmortizacion.NroCuota)
WHERE (((SP_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1007_ImpLiquidoxCICodEmpresa =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1007_ImpLiquidoxCICodEmpresa_q](@pCI INT, @pCodEmpresa NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_ImpLiquido.CI, SP_ImpLiquido.CodEmpresa, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, SP_ImpLiquido.Importe
FROM SP_ImpLiquido
WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1007_TrabajaxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1007_TrabajaxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Trabaja.CI, SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, SP_Empresa.Nombre AS DescEmpresa
FROM SP_Trabaja INNER JOIN SP_Empresa ON SP_Trabaja.CodEmpresa = SP_Empresa.CodEmpresa
WHERE (((SP_Trabaja.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1009_MinFechaVencimiento =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1009_MinFechaVencimiento_q]
AS
SELECT SP_Cuota.IDPrestamo, Min(SP_Cuota.FechaVencimiento) AS FechaVencimiento
FROM SP_Cuota
GROUP BY SP_Cuota.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: 1009_PrestamoCesion =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1009_PrestamoCesion_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Cuotas, SP_Prestamo.ImporteCuota, SP_Prestamo.Tasa, SP_Prestamo.Importe
FROM (SP_Prestamo INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI
WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1010_CtrlPrestamoEstado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1010_CtrlPrestamoEstado_q](@pCodPrestamoEstadoSig NVARCHAR(MAX), @pCodPrestamoEstadoAnt NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_CtrlPrestamoEstado.PrestamoEstadoSig, SP_CtrlPrestamoEstado.PrestamoEstadoAnt
FROM SP_CtrlPrestamoEstado
WHERE (((SP_CtrlPrestamoEstado.PrestamoEstadoSig)=@pCodPrestamoEstadoSig) AND ((SP_CtrlPrestamoEstado.PrestamoEstadoAnt)=@pCodPrestamoEstadoAnt))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1011_Prestamo_MinFechaVencimiento =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1011_Prestamo_MinFechaVencimiento_q]
AS
SELECT SP_Cuota.IDPrestamo, Min(SP_Cuota.FechaVencimiento) AS FechaVencimiento
FROM SP_Cuota
WHERE (((SP_Cuota.CodCuotaEstado)<>'anu'))
GROUP BY SP_Cuota.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: 1015_CargaLiquidos =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1015_CargaLiquidos_q]
AS
SELECT CargaLiquidos.cedula*10+ CargaLiquidos.chkdig AS CI, CargaLiquidos.imphaberes- CargaLiquidos.impdescuen AS Importe
FROM CargaLiquidos;
GO

-- ===== DATA OBJECT FOR QUERY: 1015_ImpLiquidoxEmpresaAnioMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1015_ImpLiquidoxEmpresaAnioMes_q](@pCodEmpresa INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_ImpLiquido.*
FROM SP_ImpLiquido
WHERE (((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa) AND ((SP_ImpLiquido.Mes)=@pMes) AND ((SP_ImpLiquido.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1015_TrabajaxMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1015_TrabajaxMes_q](@pCI INT, @pCodEmpresa NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, SP_Trabaja.CodEmpresa
FROM SP_Trabaja
WHERE (YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])) <= TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AND (SP_Trabaja.FechaBaja Is Null OR (YEAR([SP_Trabaja].[FechaBaja]) * 100 + MONTH([SP_Trabaja].[FechaBaja])) > TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2))) AND SP_Trabaja.CI = @pCI 
AND (YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])) <= TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) 
AND SP_Trabaja.CodEmpresa = @pCodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 1016_AfiliadoCINombre =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1016_AfiliadoCINombre_q]
AS
SELECT SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2
FROM SP_Afiliado;
GO

-- ===== DATA OBJECT FOR QUERY: 1017_LiquidoSubsidioxMesAnio =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1017_LiquidoSubsidioxMesAnio_q](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.CI, SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioCabezal.ImpLiquido) AS ImpLiquido
FROM SubsidioCabezal
WHERE (((SubsidioCabezal.Liquidar)= 1))
GROUP BY SubsidioCabezal.CI, SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1019_CargarLiquidosxMes =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1019_CargarLiquidosxMes_q]
AS
SELECT C.CEDULA AS CI, Sum(C.LIQUIDO) AS Importe
FROM CargaLiquidos AS C
GROUP BY C.CEDULA;
GO

-- ===== DATA OBJECT FOR QUERY: 1019_UItVtoCuotaxIDPrestamoEstado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1019_UItVtoCuotaxIDPrestamoEstado_q](@pIDPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Max(SP_Cuota.FechaVencimiento) AS FechaVencimiento
FROM SP_Cuota
WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1019_UltVtoCuotaNoPendienteXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1019_UltVtoCuotaNoPendienteXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT Max(SP_Cuota.FechaVencimiento) AS FechaVencimiento
FROM SP_Cuota
WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)<>'pen'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1025_PagoParcialFromPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1025_PagoParcialFromPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_PagoParcial.*
FROM SP_PagoParcial
WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1026_PagosYFacturasXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1026_PagosYFacturasXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Pago.Fecha, SP_Pago.Importe, SP_Factura.Importe AS ImporteFactura, SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Factura.Usr, SP_Factura.Ts
FROM (SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: acc_sp_1030_FacturaFlujo_q =====
CREATE OR ALTER VIEW [dbo].[acc_sp_acc_sp_1030_FacturaFlujo_q_q]
AS
SELECT SP_Factura.IdPrestamo, (YEAR(SP_Factura.FechaVencimiento) * 100 + MONTH(SP_Factura.FechaVencimiento)) AS Mes, SP_Factura.Importe
FROM SP_Factura
WHERE (((SP_Factura.CodFacturaEstado)<>'anu'));
GO

-- ===== DATA OBJECT FOR QUERY: 1030_PrestamoFlujo =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1030_PrestamoFlujo_q]
AS
SELECT SP_Prestamo.IDPrestamo, (YEAR(SP_Prestamo.FechaCobro) * 100 + MONTH(SP_Prestamo.FechaCobro)) AS Mes, -SP_Prestamo.Importe AS Importe
FROM SP_Prestamo
WHERE (((SP_Prestamo.CodPrestamoEstado)<>'anu') AND ((SP_Prestamo.FechaCobro) Is NOT Null));
GO

-- ===== DATA OBJECT FOR QUERY: 1050_CuadroAmortizacionFromID =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1050_CuadroAmortizacionFromID_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_CuadroAmortizacion.*
FROM SP_CuadroAmortizacion
WHERE (((SP_CuadroAmortizacion.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_AfiliadoNombreXCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_AfiliadoNombreXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Afiliado.CI, [SP_Afiliado].[Apellido1] + (CASE WHEN [SP_Afiliado].[Apellido2] + ''<>'' THEN ' ' + [SP_Afiliado].[Apellido2] ELSE '' END) + ', ' + [SP_Afiliado].[Nombres] AS DescAfiliado
FROM SP_Afiliado
WHERE (((SP_Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_FacturaRetenidaXNroFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_FacturaRetenidaXNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionItem.IDFactura, SP_RetencionItem.CodRetencionItemCod
FROM SP_RetencionItem INNER JOIN SP_Factura ON SP_RetencionItem.IDFactura = SP_Factura.IDFactura
WHERE (((SP_RetencionItem.CodRetencionItemCod)='fac') AND ((SP_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_PagoErrorXIDFactura =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_PagoErrorXIDFactura_q](@pIDFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_PagoError.*
FROM SP_PagoError
WHERE (((SP_PagoError.IDFactura)=@pIDFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionAvisoXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionAvisoXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionAviso.IDPrestamo, SP_RetencionAviso.Fecha, SP_RetencionAviso.Comentario, SP_RetencionAviso.Usr, SP_RetencionAviso.Ts
FROM SP_RetencionAviso
WHERE (((SP_RetencionAviso.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionesXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionesXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Retencion.IDPrestamo, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_Retencion.Importe AS Total, SP_RetencionItemCod.Descrip AS DescRetencionItemCod, SP_RetencionItem.IDFactura, SP_RetencionItem.Importe, SP_Retencion.Observaciones
FROM SP_Retencion INNER JOIN (SP_RetencionItem INNER JOIN SP_RetencionItemCod ON SP_RetencionItem.CodRetencionItemCod = SP_RetencionItemCod.CodRetencionItemCod) ON (SP_Retencion.IDPrestamo = SP_RetencionItem.IDPrestamo) AND (SP_Retencion.Fecha = SP_RetencionItem.Fecha)
WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionItemFacturaXIDPrestamoFecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionItemFacturaXIDPrestamoFecha_q](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionItem.*
FROM SP_RetencionItem
WHERE (((SP_RetencionItem.IDPrestamo)=@pIDPrestamo) AND ((SP_RetencionItem.Fecha)=@pFecha) AND ((SP_RetencionItem.CodRetencionItemCod)='fac'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionItemXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionItemXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionItem.*
FROM SP_RetencionItem
WHERE (((SP_RetencionItem.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionPagoXClave =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionPagoXClave_q](@pIDPrestamo INT, @pFecha DATETIME2(0), @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionPago.*
FROM SP_RetencionPago
WHERE (((SP_RetencionPago.IDPrestamo)=@pIDPrestamo) AND ((SP_RetencionPago.Fecha)=@pFecha) AND ((SP_RetencionPago.Mes)=@pMes) AND ((SP_RetencionPago.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionPagoXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionPagoXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionPago.*
FROM SP_RetencionPago
WHERE (((SP_RetencionPago.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionPrestamoXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionPrestamoXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_RetencionPrestamo.*
FROM SP_RetencionPrestamo
WHERE (((SP_RetencionPrestamo.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Retencion.*
FROM SP_Retencion
WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1100_RetencionXIDPrestamoFecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1100_RetencionXIDPrestamoFecha_q](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Retencion.*
FROM SP_Retencion
WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo) AND ((SP_Retencion.Fecha)=@pFecha))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1110_EmpresaPromedioXCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1110_EmpresaPromedioXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_ImpLiquido.CodEmpresa, MIN(SP_Empresa.Nombre) + ' - ' + FORMAT(Avg(SP_ImpLiquido.Importe),'0.00') AS Descrip
FROM (SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)) INNER JOIN SP_Empresa ON SP_ImpLiquido.CodEmpresa = SP_Empresa.CodEmpresa
WHERE (((SP_ImpLiquido.AnioMes) Between (YEAR(DATEADD(month,-5,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-5,CAST(GETDATE() AS date)))) And (YEAR(DATEADD(month,-2,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-2,CAST(GETDATE() AS date))))) AND ((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null))
GROUP BY SP_ImpLiquido.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 1110_FacturasVenciasxIDPrestamoFecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1110_FacturasVenciasxIDPrestamoFecha_q](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.NroFactura, SP_Factura.FechaVencimiento, SP_Factura.Importe
FROM SP_Factura
WHERE (((SP_Factura.FechaVencimiento)<@pFecha) AND ((SP_Factura.FechaPago) Is Null) AND ((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)<>'anu'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1120_Retencion_Amort =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1120_Retencion_Amort_q]
AS
SELECT SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha, Sum(SP_Factura.Importe*SP_Retencion.TipoCambio) AS Importe, Sum(SP_Factura.ImpAmortizable*SP_Retencion.TipoCambio) AS ImpAmortizable, Sum(SP_Factura.ImpInteres*SP_Retencion.TipoCambio) AS ImpInteres, Sum(SP_Pago.Importe*SP_Retencion.TipoCambio)-Sum(SP_Factura.Importe*SP_Retencion.TipoCambio) AS ImpMora
FROM (SP_Retencion INNER JOIN ((SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_RetencionItem ON SP_Pago.IDFactura = SP_RetencionItem.IDFactura) ON (SP_Retencion.IDPrestamo = SP_RetencionItem.IDPrestamo) AND (SP_Retencion.Fecha = SP_RetencionItem.Fecha)) INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo
GROUP BY SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha;
GO

-- ===== DATA OBJECT FOR QUERY: 1120_Retencion_Amort111 =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1120_Retencion_Amort111_q]
AS
SELECT SP_Retencion.IDPrestamo, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CI, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_Retencion.Importe, SP_Retencion.Observaciones, SP_Retencion.Usr, SP_Retencion.Ts
FROM (SP_Retencion INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: 1120_Retencion_Tel =====
CREATE OR ALTER VIEW [dbo].[acc_sp_1120_Retencion_Tel_q]
AS
SELECT SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha, Sum(SP_RetencionItem.Importe) AS Importe
FROM SP_RetencionItem
WHERE (((SP_RetencionItem.CodRetencionItemCod)='tel'))
GROUP BY SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha;
GO

-- ===== DATA OBJECT FOR QUERY: 1130_FacturaInteresMes_Orig =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1130_FacturaInteresMes_Orig_q](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IdPrestamo, TRY_CONVERT(datetime2,'01/' + FORMAT(SP_Factura.FechaVencimiento,'mm/yy')) AS Mes, SP_Factura.ImpInteres
FROM SP_Factura
WHERE (((SP_Factura.CodFacturaEstado)<>'anu') AND ((SP_Factura.CodMoneda)=@pCodMoneda))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1140_FacturaDetallexIDPrestamoCodItemPago =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1140_FacturaDetallexIDPrestamoCodItemPago_q](@pIDPrestamo INT, @pCodItemPago NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IdPrestamo, SP_FacturaDetalle.CodItemPago
FROM SP_Factura INNER JOIN SP_FacturaDetalle ON SP_Factura.IDFactura = SP_FacturaDetalle.IdFactura
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_FacturaDetalle.CodItemPago)=@pCodItemPago))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1150_AfiliadoCometarioXCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1150_AfiliadoCometarioXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_AfiliadoComentario.*
FROM SP_AfiliadoComentario
WHERE (((SP_AfiliadoComentario.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1200_CuotasPendientesXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1200_CuotasPendientesXIDPrestamo_q](@pIDPrestamo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
FROM SP_Cuota
WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)='pen'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1200_FacturasPendientesXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1200_FacturasPendientesXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Factura.IdPrestamo, SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.Impresiones, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts
FROM SP_Factura
WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)='emi'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1234_PagoParcialPorID =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1234_PagoParcialPorID_q](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SP_PagoParcial.*
FROM SP_PagoParcial
WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo) AND ((SP_PagoParcial.Fecha)=@pFecha))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1234_PagosParcialesXIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1234_PagosParcialesXIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_PagoParcial.*
FROM SP_PagoParcial
WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_AfiliadoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_AfiliadoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Afiliado.Telefono
FROM SP_Afiliado
WHERE (((SP_Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_AutorizacionxIDPrestamo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_AutorizacionxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.IDPrestamo, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2
FROM SP_Afiliado INNER JOIN SP_Prestamo ON SP_Afiliado.CI = SP_Prestamo.CI
WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 999_Parametros =====
CREATE OR ALTER FUNCTION [dbo].[acc_sp_999_Parametros_q](@pLogin NVARCHAR(MAX), @pClave NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT xPar.login, xPar.Clave, xPar.orden, xPar.value1, xPar.value2, xPar.value3, xPar.value4, xPar.value5
FROM xUsrParam AS xPar
WHERE (((xPar.login)=@pLogin) AND ((xPar.Clave)=@pClave))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Factura =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Factura_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Prestamo.Tasa, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_FacturaDetalle.Descrip, SP_FacturaDetalle.Importe, SP_FacturaDetalle.NroCuota, SP_Factura.CodigoBarra, SP_Factura.CodFacturaEstado, SP_Factura.Impresiones, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.CodFacturaTipo, SP_FacturaTipo.Descrip AS DescFacturaTipo FROM ((((SP_Afiliado INNER JOIN SP_Prestamo ON SP_Afiliado.CI = SP_Prestamo.CI) INNER JOIN SP_Factura ON SP_Prestamo.IDPrestamo = SP_Factura.IdPrestamo) INNER JOIN SP_FacturaDetalle ON SP_Factura.IDFactura = SP_FacturaDetalle.IdFactura) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaTipo ON SP_Factura.CodFacturaTipo = SP_FacturaTipo.CodFacturaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Factura_DBG =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Factura_DBG_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Afiliado.Telefono, SP_Afiliado.EMail, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.CodFacturaTipo, SP_FacturaTipo.Descrip AS DescFacturaTipo
FROM (((SP_Afiliado INNER JOIN (SP_Factura INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) ON SP_Afiliado.CI = SP_Prestamo.CI) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado) INNER JOIN SP_FacturaTipo ON SP_Factura.CodFacturaTipo = SP_FacturaTipo.CodFacturaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Pago =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Pago_q]
AS
SELECT SP_Factura.NroFactura, SP_Prestamo.IDPrestamo, SP_Pago.Importe, SP_Factura.Importe AS ImpFactura, (CASE WHEN [SP_Pago].[Importe]>SP_Factura.Importe THEN [SP_Pago].[Importe]-SP_Factura.Importe ELSE 0 END) AS ImpInteresMora, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Factura.FechaVencimiento, SP_Pago.Fecha AS FechaPago, SP_Pago.CodSucursal, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Pago.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Pago.Usr, SP_Pago.Ts
FROM ((((SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_PagoError =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_PagoError_q]
AS
SELECT SP_PagoError.IDFactura, SP_PagoError.Fecha, SP_PagoError.Importe, SP_PagoError.CodSucursal, SP_PagoError.TasaCambio, SP_PagoError.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_PagoError.Usr, SP_PagoError.Ts
FROM SP_PagoError INNER JOIN SP_FacturaEstado ON SP_PagoError.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_PrestamoCuadro =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_PrestamoCuadro_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Moneda.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_CuadroAmortizacion.NroCuota, SP_CuadroAmortizacion.Monto, SP_CuadroAmortizacion.ImporteCuota, SP_CuadroAmortizacion.Interes, SP_CuadroAmortizacion.Amortizacion, SP_CuadroAmortizacion.Saldo, SP_Prestamo.Promedio, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo
FROM (((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_CuadroAmortizacion ON SP_Prestamo.IDPrestamo = SP_CuadroAmortizacion.IDPrestamo) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_RetencionPago =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_RetencionPago_q]
AS
SELECT SP_RetencionPago.IDPrestamo, SP_RetencionPago.Fecha, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPago.Mes, SP_RetencionPago.Anio, SP_RetencionPago.Importe, SP_RetencionPago.Usr, SP_RetencionPago.Ts
FROM (SP_RetencionPago INNER JOIN SP_RetencionPrestamo ON SP_RetencionPago.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_RetencionPrestamo =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_RetencionPrestamo_q]
AS
SELECT SP_RetencionPrestamo.IDPrestamo, SP_RetencionPrestamo.CI, SP_RetencionPrestamo.Fecha, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CodMoneda, SP_RetencionPrestamo.Importe, SP_RetencionPrestamo.Saldo, SP_RetencionPrestamo.ImpPago, SP_RetencionPrestamo.Usr, SP_RetencionPrestamo.Ts
FROM SP_RetencionPrestamo INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoComentario =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_AfiliadoComentario_q]
AS
SELECT SP_AfiliadoComentario.*
FROM SP_AfiliadoComentario;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Empresa_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_Empresa_Descrip_q]
AS
SELECT SP_Empresa.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa
FROM SP_Empresa
WHERE (((SP_Empresa.Ficticia)= 0));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_FacturaEstado_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_FacturaEstado_Descrip_q]
AS
SELECT SP_FacturaEstado.CodFacturaEstado, SP_FacturaEstado.Descrip
FROM SP_FacturaEstado;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_FacturaTipo_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_FacturaTipo_Descrip_q]
AS
SELECT SP_FacturaTipo.CodFacturaTipo, SP_FacturaTipo.Descrip
FROM SP_FacturaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Moneda_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_Moneda_Descrip_q]
AS
SELECT SP_Moneda.CodMoneda, SP_Moneda.Descrip, SP_Moneda.Tasa
FROM SP_Moneda;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PagoOrigen_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PagoOrigen_Descrip_q]
AS
SELECT SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip
FROM SP_PagoOrigen;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoActivo =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PrestamoActivo_q]
AS
SELECT SP_Prestamo.*
FROM SP_PrestamoEstado INNER JOIN SP_Prestamo ON SP_PrestamoEstado.CodPrestamoEstado = SP_Prestamo.CodPrestamoEstado
WHERE (((SP_PrestamoEstado.Fin)= 0));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoEstado_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PrestamoEstado_Descrip_q]
AS
SELECT SP_PrestamoEstado.CodPrestamoEstado, SP_PrestamoEstado.Descrip
FROM SP_PrestamoEstado;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoInteresAmortizacion =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PrestamoInteresAmortizacion_q]
AS
SELECT SP_CuadroAmortizacion.IDPrestamo, Sum(SP_CuadroAmortizacion.Interes) AS Interes, Sum(SP_CuadroAmortizacion.Amortizacion) AS Amortizacion
FROM SP_CuadroAmortizacion
GROUP BY SP_CuadroAmortizacion.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoPctRetenciones =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PrestamoPctRetenciones_q]
AS
SELECT P.IDPrestamo, MIN(P.CI) AS CI, MIN(P.Fecha) AS Fecha, MIN(P.CodMoneda) AS CodMoneda, MIN(P.Importe) AS Importe, 0 AS Tasa, MIN(SP_PrestamoEstado.Descrip) AS DescPrestamoEstado, Count(*) AS Cant_Facturas, Sum((CASE WHEN F.CodFacturaEstado='ret' THEN 1 ELSE 0 END)) AS Cant_Fac_Ret, CASE WHEN Count(*)=0 THEN 0 ELSE Sum((CASE WHEN F.CodFacturaEstado='ret' THEN 1 ELSE 0 END))*1.0/Count(*) END AS Pct_Retenidas
FROM (SP_Prestamo AS P INNER JOIN SP_Factura AS F ON P.IDPrestamo = F.IdPrestamo) INNER JOIN SP_PrestamoEstado ON P.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado
WHERE (((SP_PrestamoEstado.Fin)= 1))
GROUP BY P.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoTipo_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sp_Rs_PrestamoTipo_Descrip_q]
AS
SELECT SP_PrestamoTipo.CodPrestamoTipo, SP_PrestamoTipo.Descrip
FROM SP_PrestamoTipo;
GO

-- ===== DATA OBJECT FOR QUERY: wFactura =====
CREATE OR ALTER VIEW [dbo].[acc_sp_wFactura_q]
AS
SELECT SP_Factura.*, SP_Prestamo_1.Fecha AS Fecha_Pre
FROM ((SP_Factura INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) INNER JOIN SP_Prestamo AS SP_Prestamo_1 ON SP_Prestamo.IDPrestamoRef = SP_Prestamo_1.IDPrestamo) LEFT JOIN SP_Pago ON SP_Factura.IDFactura = SP_Pago.IDFactura
WHERE (((SP_Factura.CodFacturaEstado)='car') AND ((SP_Pago.IDFactura) Is Null));
GO

-- ===== DATA OBJECT FOR QUERY: wRetencionDetalle =====
CREATE OR ALTER VIEW [dbo].[acc_sp_wRetencionDetalle_q]
AS
SELECT IDPrestamo, Fecha, -SP_RetencionPago.Importe AS Importe
FROM SP_RetencionPago
UNION ALL SELECT IDPrestamo, Fecha, Importe 
FROM SP_Retencion;
GO

-- ===== DATA OBJECT FOR QUERY: xw_FacturasCantidadXPrestamo =====
CREATE OR ALTER VIEW [dbo].[acc_sp_xw_FacturasCantidadXPrestamo_q]
AS
SELECT SP_Factura.IdPrestamo, Count(SP_Factura.IDFactura) AS CuentaDeIDFactura
FROM SP_Factura
GROUP BY SP_Factura.IdPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: xw_FacturasDeCancelacionAnticipada =====
CREATE OR ALTER VIEW [dbo].[acc_sp_xw_FacturasDeCancelacionAnticipada_q]
AS
SELECT SP_Factura.IdPrestamo, Max(SP_Factura.IDFactura) AS IDFactura
FROM tmp_Anticipados AS fa INNER JOIN SP_Factura ON fa.IDPrestamo = SP_Factura.IdPrestamo
GROUP BY SP_Factura.IdPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: 1000_AfiliadoPromedioMeses =====
-- DependsOn: 1000_AfiliadoImpLiquidoxMes
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1000_AfiliadoPromedioMeses_q](@pCI INT, @pAnioMesIni INT, @pAnioMesFin INT, @pMeses NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Sum(acc_sp_1000_AfiliadoImpLiquidoxMes_q.Importe)/@pMeses AS Importe
FROM [acc_sp_1000_AfiliadoImpLiquidoxMes_q]
WHERE (((acc_sp_1000_AfiliadoImpLiquidoxMes_q.CI)=@pCI) AND ((acc_sp_1000_AfiliadoImpLiquidoxMes_q.AnioMes) Between @pAnioMesIni And @pAnioMesFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1009_PrestamoVale =====
-- DependsOn: 1009_MinFechaVencimiento
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1009_PrestamoVale_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Cuotas, SP_Prestamo.ImporteCuota, acc_sp_1009_MinFechaVencimiento_q.FechaVencimiento, SP_Prestamo.Tasa, SP_Prestamo.Importe
FROM ((SP_Prestamo INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN [acc_sp_1009_MinFechaVencimiento_q] ON SP_Prestamo.IDPrestamo = acc_sp_1009_MinFechaVencimiento_q.IDPrestamo
WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Prestamo_Crystal =====
-- DependsOn: 1011_Prestamo_MinFechaVencimiento
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Prestamo_Crystal_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, acc_sp_1011_Prestamo_MinFechaVencimiento_q.FechaVencimiento, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo
FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) LEFT JOIN [acc_sp_1011_Prestamo_MinFechaVencimiento_q] ON SP_Prestamo.IDPrestamo = acc_sp_1011_Prestamo_MinFechaVencimiento_q.IDPrestamo) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 1025_TotalPagoParcialPorPrestamo =====
-- DependsOn: 1025_PagoParcialFromPrestamo
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1025_TotalPagoParcialPorPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum([1025_PagoParcialFromPrestamo].Importe) AS Importe
FROM [acc_sp_1025_PagoParcialFromPrestamo_q](@pIDPrestamo) AS [1025_PagoParcialFromPrestamo]
)
GO

-- ===== DATA OBJECT FOR QUERY: 1030_FlujoTIR =====
-- DependsOn: 1030_PrestamoFlujo, acc_sp_1030_FacturaFlujo_q
CREATE OR ALTER VIEW [dbo].[acc_sp_1030_FlujoTIR_q]
AS
SELECT * FROM [acc_sp_1030_PrestamoFlujo_q]
UNION ALL SELECT * FROM [acc_sp_1030_FacturaFlujo_q];
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Retencion =====
-- DependsOn: 1120_Retencion_Amort, 1120_Retencion_Tel
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Retencion_q]
AS
SELECT SP_Retencion.IDPrestamo, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CI, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_RetencionPrestamo.CodMoneda, acc_sp_1120_Retencion_Amort_q.Importe, acc_sp_1120_Retencion_Amort_q.ImpAmortizable, acc_sp_1120_Retencion_Amort_q.ImpInteres, acc_sp_1120_Retencion_Amort_q.ImpMora, acc_sp_1120_Retencion_Tel_q.Importe AS ImpTel, SP_Retencion.Observaciones, SP_Retencion.Usr, SP_Retencion.Ts
FROM (((SP_Retencion INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa) INNER JOIN [acc_sp_1120_Retencion_Amort_q] ON (SP_Retencion.Fecha = acc_sp_1120_Retencion_Amort_q.Fecha) AND (SP_Retencion.IDPrestamo = acc_sp_1120_Retencion_Amort_q.IDPrestamo)) LEFT JOIN [acc_sp_1120_Retencion_Tel_q] ON (SP_Retencion.Fecha = acc_sp_1120_Retencion_Tel_q.Fecha) AND (SP_Retencion.IDPrestamo = acc_sp_1120_Retencion_Tel_q.IDPrestamo);
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_FacturaxIDPrestamo =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.CodigoBarra, Rpt_Factura.Impresiones FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
WHERE (((Rpt_Factura.IDPrestamo)=@pIDPrestamo) AND ((Rpt_Factura.CodFacturaEstado)<>'anu' And (Rpt_Factura.CodFacturaEstado)<>'can'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_FacturaxIDPrestamoEstado =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamoEstado_q](@pIDPrestamo INT, @pCodFacturaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.CodigoBarra, Rpt_Factura.CodFacturaEstado, Rpt_Factura.Impresiones FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
WHERE (((Rpt_Factura.IDPrestamo)=@pIDPrestamo) AND ((Rpt_Factura.CodFacturaEstado)=@pCodFacturaEstado))
)
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_FacturaxNroFactura =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_FacturaxNroFactura_q](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.CodigoBarra, Rpt_Factura.Impresiones FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
WHERE (((Rpt_Factura.NroFactura)=@pNroFactura))
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Tmp_Rpt_Factura =====
-- DependsOn: Rpt_Factura_DBG
CREATE OR ALTER VIEW [dbo].[acc_sp_500_Tmp_Rpt_Factura_q]
AS
SELECT *
FROM [acc_sp_Rpt_Factura_DBG_q] AS Rpt_Factura
WHERE ( [Rpt_Factura].[FechaEmitida] <= TRY_CONVERT(datetime2,'31/12/2007') and ([Rpt_Factura].[FechaPago] > TRY_CONVERT(datetime2,'31/12/2007') or [Rpt_Factura].[FechaPago] Is Null) and [Rpt_Factura].[CodFacturaEstado] <> 'anu' and [Rpt_Factura].[CodFacturaEstado] <> 'cle' );
GO

-- ===== DATA OBJECT FOR QUERY: 2000_Rpt_PrestamoCuadroxIDPrestamo =====
-- DependsOn: Rpt_PrestamoCuadro
CREATE OR ALTER FUNCTION [dbo].[acc_sp_2000_Rpt_PrestamoCuadroxIDPrestamo_q](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT Rpt_PrestamoCuadro.*
FROM [acc_sp_Rpt_PrestamoCuadro_q] AS Rpt_PrestamoCuadro
WHERE (((Rpt_PrestamoCuadro.IDPrestamo)=@pIDPrestamo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1116_AfiliadoComentarioXCI =====
-- DependsOn: Rs_AfiliadoComentario
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1116_AfiliadoComentarioXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Rs_AfiliadoComentario.*
FROM [acc_sp_Rs_AfiliadoComentario_q] AS Rs_AfiliadoComentario
WHERE (((Rs_AfiliadoComentario.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1022_ImporteCuotaPrestamoActivoXCI =====
-- DependsOn: Rs_PrestamoActivo
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1022_ImporteCuotaPrestamoActivoXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT TOP 1 SP_Cuota.Importe, acc_sp_Rs_PrestamoActivo_q.CodMoneda
FROM ([acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q INNER JOIN SP_Cuota ON acc_sp_Rs_PrestamoActivo_q.IDPrestamo = SP_Cuota.IDPrestamo)
WHERE (((acc_sp_Rs_PrestamoActivo_q.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 1022_PrestamoActivoXCI =====
-- DependsOn: Rs_PrestamoActivo
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1022_PrestamoActivoXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT acc_sp_Rs_PrestamoActivo_q.*
FROM [acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q
WHERE (((acc_sp_Rs_PrestamoActivo_q.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Prestamo =====
-- DependsOn: Rs_PrestamoInteresAmortizacion
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Prestamo_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.IDPrestamoRef, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo, acc_sp_Rs_PrestamoInteresAmortizacion_q.Amortizacion, acc_sp_Rs_PrestamoInteresAmortizacion_q.Interes
FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo) LEFT JOIN [acc_sp_Rs_PrestamoInteresAmortizacion_q] ON SP_Prestamo.IDPrestamo = acc_sp_Rs_PrestamoInteresAmortizacion_q.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Prestamo2 =====
-- DependsOn: Rs_PrestamoInteresAmortizacion
CREATE OR ALTER VIEW [dbo].[acc_sp_Rpt_Prestamo2_q]
AS
SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.IDPrestamoRef, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo, acc_sp_Rs_PrestamoInteresAmortizacion_q.Interes, acc_sp_Rs_PrestamoInteresAmortizacion_q.Amortizacion
FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo) INNER JOIN [acc_sp_Rs_PrestamoInteresAmortizacion_q] ON SP_Prestamo.IDPrestamo = acc_sp_Rs_PrestamoInteresAmortizacion_q.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: 1115_PrestamosAnterioresxCI =====
-- DependsOn: Rs_PrestamoPctRetenciones
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1115_PrestamosAnterioresxCI_q](@pCI INT, @pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
SELECT acc_sp_Rs_PrestamoPctRetenciones_q.*
FROM [acc_sp_Rs_PrestamoPctRetenciones_q]
WHERE (((acc_sp_Rs_PrestamoPctRetenciones_q.IDPrestamo)<(CASE WHEN @pIDPrestamo>0 THEN @pIDPrestamo ELSE 999999999 END)) AND ((acc_sp_Rs_PrestamoPctRetenciones_q.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: wSaldoSum =====
-- DependsOn: wRetencionDetalle
CREATE OR ALTER VIEW [dbo].[acc_sp_wSaldoSum_q]
AS
SELECT wRetencionDetalle.IDPrestamo, TRY_CONVERT(int, Sum(wRetencionDetalle.Importe)) AS SumaDeImporte
FROM [acc_sp_wRetencionDetalle_q] AS wRetencionDetalle INNER JOIN SP_RetencionPrestamo ON wRetencionDetalle.IDPrestamo = SP_RetencionPrestamo.IDPrestamo
WHERE (((wRetencionDetalle.Fecha)<CONVERT(date, '2006-01-01')))
GROUP BY wRetencionDetalle.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: xw_PrestamosAnticipados =====
-- DependsOn: xw_FacturasCantidadXPrestamo
CREATE OR ALTER VIEW [dbo].[acc_sp_xw_PrestamosAnticipados_q]
AS
SELECT SP_Prestamo.IDPrestamo
FROM SP_Prestamo INNER JOIN [acc_sp_xw_FacturasCantidadXPrestamo_q] ON SP_Prestamo.IDPrestamo = acc_sp_xw_FacturasCantidadXPrestamo_q.IdPrestamo
WHERE (((acc_sp_xw_FacturasCantidadXPrestamo_q.CuentaDeIDFactura)>[Cuotas]));
GO

-- ===== DATA OBJECT FOR QUERY: Buscar duplicados por 1030_FlujoTIR =====
-- DependsOn: 1030_FlujoTIR
CREATE OR ALTER VIEW [dbo].[acc_sp_Buscar_duplicados_por_1030_FlujoTIR_q]
AS
SELECT DISTINCT [IDPrestamo], [Mes], [Importe]
FROM [acc_sp_1030_FlujoTIR_q]
WHERE [IDPrestamo] In (SELECT [IDPrestamo] FROM [acc_sp_1030_FlujoTIR_q] As Tmp GROUP BY [IDPrestamo],[Mes] HAVING Count(*)>1  And [Mes] = acc_sp_1030_FlujoTIR_q.[Mes]);
GO

-- ===== DATA OBJECT FOR QUERY: 1130_FacturaInteresMes =====
-- DependsOn: 500_Tmp_Rpt_Factura
CREATE OR ALTER FUNCTION [dbo].[acc_sp_1130_FacturaInteresMes_q](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT IdPrestamo, TRY_CONVERT(datetime2,'01/' + FORMAT(FechaVencimiento,'mm/yy')) AS Mes, ImpInteres
FROM [acc_sp_500_Tmp_Rpt_Factura_q]
WHERE (((CodFacturaEstado)<>'anu') AND ((CodMoneda)=@pCodMoneda))
)
GO

-- ===== COMPAT OBJECT FOR QUERY: 1000_AfiladoCI2Nombre =====
IF OBJECT_ID('dbo.1000_AfiladoCI2Nombre') IS NULL EXEC('CREATE FUNCTION [dbo].[1000_AfiladoCI2Nombre](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1000_AfiladoCI2Nombre_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1000_AfiliadoImpLiquidoxMes =====
IF OBJECT_ID('dbo.1000_AfiliadoImpLiquidoxMes') IS NULL EXEC('CREATE VIEW [dbo].[1000_AfiliadoImpLiquidoxMes] AS SELECT * FROM [dbo].[acc_sp_1000_AfiliadoImpLiquidoxMes_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1000_PrestamoxIDPrestamo =====
IF OBJECT_ID('dbo.1000_PrestamoxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1000_PrestamoxIDPrestamo](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1000_PrestamoxIDPrestamo_q](@pIdPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1001_AfiliadoImpLiquidoxCI =====
IF OBJECT_ID('dbo.1001_AfiliadoImpLiquidoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1001_AfiliadoImpLiquidoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1001_AfiliadoImpLiquidoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1001_FacturaIdMax =====
IF OBJECT_ID('dbo.1001_FacturaIdMax') IS NULL EXEC('CREATE VIEW [dbo].[1001_FacturaIdMax] AS SELECT * FROM [dbo].[acc_sp_1001_FacturaIdMax_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1001_FacturaMax =====
IF OBJECT_ID('dbo.1001_FacturaMax') IS NULL EXEC('CREATE VIEW [dbo].[1001_FacturaMax] AS SELECT * FROM [dbo].[acc_sp_1001_FacturaMax_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1001_PrestamoMax =====
IF OBJECT_ID('dbo.1001_PrestamoMax') IS NULL EXEC('CREATE VIEW [dbo].[1001_PrestamoMax] AS SELECT * FROM [dbo].[acc_sp_1001_PrestamoMax_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_CuadroAmortizacionxIDPrestamo =====
IF OBJECT_ID('dbo.1002_CuadroAmortizacionxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_CuadroAmortizacionxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_CuadroAmortizacionxIDPrestamoNroCuota =====
IF OBJECT_ID('dbo.1002_CuadroAmortizacionxIDPrestamoNroCuota') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_CuadroAmortizacionxIDPrestamoNroCuota](@pIdPrestamo INT, @pNroCuota NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamoNroCuota_q](@pIdPrestamo, @pNroCuota)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_CuotasxIDPrestamo =====
IF OBJECT_ID('dbo.1002_CuotasxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_CuotasxIDPrestamo](@pIdPrestamo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_CuotasxIDPrestamo_q](@pIdPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_CuotasxIDPrestamoCuotaEstado =====
IF OBJECT_ID('dbo.1002_CuotasxIDPrestamoCuotaEstado') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_CuotasxIDPrestamoCuotaEstado](@pIdPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_CuotasxIDPrestamoCuotaEstado_q](@pIdPrestamo, @pCodCuotaEstado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_CuotaxIDPrestamoNro =====
IF OBJECT_ID('dbo.1002_CuotaxIDPrestamoNro') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_CuotaxIDPrestamoNro](@pIdPrestamo INT, @pNro NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_CuotaxIDPrestamoNro_q](@pIdPrestamo, @pNro)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturaDetallexIDFactura =====
IF OBJECT_ID('dbo.1002_FacturaDetallexIDFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturaDetallexIDFactura](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturaDetallexIDFactura_q](@pIdFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturaDetallexIDFacturaCodItemPago =====
IF OBJECT_ID('dbo.1002_FacturaDetallexIDFacturaCodItemPago') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturaDetallexIDFacturaCodItemPago](@pIdFactura INT, @pCodItemPago NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturaDetallexIDFacturaCodItemPago_q](@pIdFactura, @pCodItemPago)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturasEmitidasXIDPrestamo =====
IF OBJECT_ID('dbo.1002_FacturasEmitidasXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturasEmitidasXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturasEmitidasXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturasxIDPrestamo =====
IF OBJECT_ID('dbo.1002_FacturasxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturasxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturasxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturaxIDFactura =====
IF OBJECT_ID('dbo.1002_FacturaxIDFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturaxIDFactura](@pIDFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturaxIDFactura_q](@pIDFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_FacturaxNroFactura =====
IF OBJECT_ID('dbo.1002_FacturaxNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_FacturaxNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_FacturaxNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_LiquidoAnioMesxCI =====
IF OBJECT_ID('dbo.1002_LiquidoAnioMesxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_LiquidoAnioMesxCI](@pCI INT, @pMesIni INT, @pMesFin INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_LiquidoAnioMesxCI_q](@pCI, @pMesIni, @pMesFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_PagoItemPagoxIDFactura =====
IF OBJECT_ID('dbo.1002_PagoItemPagoxIDFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_PagoItemPagoxIDFactura](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_PagoItemPagoxIDFactura_q](@pIdFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_PagoxIDFactura =====
IF OBJECT_ID('dbo.1002_PagoxIDFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_PagoxIDFactura](@pIdFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_PagoxIDFactura_q](@pIdFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_PrestamoAbiertoxCI =====
IF OBJECT_ID('dbo.1002_PrestamoAbiertoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_PrestamoAbiertoxCI](@pCI INT, @pCodPrestamoTipo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_PrestamoAbiertoxCI_q](@pCI, @pCodPrestamoTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_PrestamoxIDPrestamo =====
IF OBJECT_ID('dbo.1002_PrestamoxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_PrestamoxIDPrestamo](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_PrestamoxIDPrestamo_q](@pIdPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1002_TasasxCodMoneda =====
IF OBJECT_ID('dbo.1002_TasasxCodMoneda') IS NULL EXEC('CREATE FUNCTION [dbo].[1002_TasasxCodMoneda](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1002_TasasxCodMoneda_q](@pCodMoneda)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_CuotasPendientes =====
IF OBJECT_ID('dbo.1003_CuotasPendientes') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_CuotasPendientes](@pIdPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_CuotasPendientes_q](@pIdPrestamo, @pCodCuotaEstado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_CuotaxIDPrestamoNro =====
IF OBJECT_ID('dbo.1003_CuotaxIDPrestamoNro') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_CuotaxIDPrestamoNro](@pIDPrestamo INT, @pNro NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_CuotaxIDPrestamoNro_q](@pIDPrestamo, @pNro)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_CuotaxNroFactura =====
IF OBJECT_ID('dbo.1003_CuotaxNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_CuotaxNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_CuotaxNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_FacturasxIDPrestamo =====
IF OBJECT_ID('dbo.1003_FacturasxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_FacturasxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_FacturasxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_FacturasxIDPrestamoEstado =====
IF OBJECT_ID('dbo.1003_FacturasxIDPrestamoEstado') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_FacturasxIDPrestamoEstado](@pIDPrestamo INT, @pCodFacturaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_FacturasxIDPrestamoEstado_q](@pIDPrestamo, @pCodFacturaEstado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_PagosSingleXIDPrestamo =====
IF OBJECT_ID('dbo.1003_PagosSingleXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_PagosSingleXIDPrestamo](@pIdPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_PagosSingleXIDPrestamo_q](@pIdPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_PagosxIDPrestamo =====
IF OBJECT_ID('dbo.1003_PagosxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_PagosxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_PagosxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_PagoxNroFactura =====
IF OBJECT_ID('dbo.1003_PagoxNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_PagoxNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_PagoxNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1003_SaldoxNroFactura =====
IF OBJECT_ID('dbo.1003_SaldoxNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1003_SaldoxNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1003_SaldoxNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1007_ImpLiquidoxCICodEmpresa =====
IF OBJECT_ID('dbo.1007_ImpLiquidoxCICodEmpresa') IS NULL EXEC('CREATE FUNCTION [dbo].[1007_ImpLiquidoxCICodEmpresa](@pCI INT, @pCodEmpresa NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1007_ImpLiquidoxCICodEmpresa_q](@pCI, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1007_TrabajaxCI =====
IF OBJECT_ID('dbo.1007_TrabajaxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1007_TrabajaxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1007_TrabajaxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1009_MinFechaVencimiento =====
IF OBJECT_ID('dbo.1009_MinFechaVencimiento') IS NULL EXEC('CREATE VIEW [dbo].[1009_MinFechaVencimiento] AS SELECT * FROM [dbo].[acc_sp_1009_MinFechaVencimiento_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1009_PrestamoCesion =====
IF OBJECT_ID('dbo.1009_PrestamoCesion') IS NULL EXEC('CREATE FUNCTION [dbo].[1009_PrestamoCesion](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1009_PrestamoCesion_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1010_CtrlPrestamoEstado =====
IF OBJECT_ID('dbo.1010_CtrlPrestamoEstado') IS NULL EXEC('CREATE FUNCTION [dbo].[1010_CtrlPrestamoEstado](@pCodPrestamoEstadoSig NVARCHAR(MAX), @pCodPrestamoEstadoAnt NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1010_CtrlPrestamoEstado_q](@pCodPrestamoEstadoSig, @pCodPrestamoEstadoAnt)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1011_Prestamo_MinFechaVencimiento =====
IF OBJECT_ID('dbo.1011_Prestamo_MinFechaVencimiento') IS NULL EXEC('CREATE VIEW [dbo].[1011_Prestamo_MinFechaVencimiento] AS SELECT * FROM [dbo].[acc_sp_1011_Prestamo_MinFechaVencimiento_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1015_CargaLiquidos =====
IF OBJECT_ID('dbo.1015_CargaLiquidos') IS NULL EXEC('CREATE VIEW [dbo].[1015_CargaLiquidos] AS SELECT * FROM [dbo].[acc_sp_1015_CargaLiquidos_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1015_ImpLiquidoxEmpresaAnioMes =====
IF OBJECT_ID('dbo.1015_ImpLiquidoxEmpresaAnioMes') IS NULL EXEC('CREATE FUNCTION [dbo].[1015_ImpLiquidoxEmpresaAnioMes](@pCodEmpresa INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1015_ImpLiquidoxEmpresaAnioMes_q](@pCodEmpresa, @pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1015_TrabajaxMes =====
IF OBJECT_ID('dbo.1015_TrabajaxMes') IS NULL EXEC('CREATE FUNCTION [dbo].[1015_TrabajaxMes](@pCI INT, @pCodEmpresa NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1015_TrabajaxMes_q](@pCI, @pCodEmpresa, @pAnio, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1016_AfiliadoCINombre =====
IF OBJECT_ID('dbo.1016_AfiliadoCINombre') IS NULL EXEC('CREATE VIEW [dbo].[1016_AfiliadoCINombre] AS SELECT * FROM [dbo].[acc_sp_1016_AfiliadoCINombre_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1017_LiquidoSubsidioxMesAnio =====
IF OBJECT_ID('dbo.1017_LiquidoSubsidioxMesAnio') IS NULL EXEC('CREATE FUNCTION [dbo].[1017_LiquidoSubsidioxMesAnio](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1017_LiquidoSubsidioxMesAnio_q](@pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1019_CargarLiquidosxMes =====
IF OBJECT_ID('dbo.1019_CargarLiquidosxMes') IS NULL EXEC('CREATE VIEW [dbo].[1019_CargarLiquidosxMes] AS SELECT * FROM [dbo].[acc_sp_1019_CargarLiquidosxMes_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1019_UItVtoCuotaxIDPrestamoEstado =====
IF OBJECT_ID('dbo.1019_UItVtoCuotaxIDPrestamoEstado') IS NULL EXEC('CREATE FUNCTION [dbo].[1019_UItVtoCuotaxIDPrestamoEstado](@pIDPrestamo INT, @pCodCuotaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1019_UItVtoCuotaxIDPrestamoEstado_q](@pIDPrestamo, @pCodCuotaEstado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1019_UltVtoCuotaNoPendienteXIDPrestamo =====
IF OBJECT_ID('dbo.1019_UltVtoCuotaNoPendienteXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1019_UltVtoCuotaNoPendienteXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1019_UltVtoCuotaNoPendienteXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1025_PagoParcialFromPrestamo =====
IF OBJECT_ID('dbo.1025_PagoParcialFromPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1025_PagoParcialFromPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1025_PagoParcialFromPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1026_PagosYFacturasXIDPrestamo =====
IF OBJECT_ID('dbo.1026_PagosYFacturasXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1026_PagosYFacturasXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1026_PagosYFacturasXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: acc_sp_1030_FacturaFlujo_q =====
IF OBJECT_ID('dbo.acc_sp_1030_FacturaFlujo_q') IS NULL EXEC('CREATE VIEW [dbo].[acc_sp_1030_FacturaFlujo_q] AS SELECT * FROM [dbo].[acc_sp_acc_sp_1030_FacturaFlujo_q_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1030_PrestamoFlujo =====
IF OBJECT_ID('dbo.1030_PrestamoFlujo') IS NULL EXEC('CREATE VIEW [dbo].[1030_PrestamoFlujo] AS SELECT * FROM [dbo].[acc_sp_1030_PrestamoFlujo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1050_CuadroAmortizacionFromID =====
IF OBJECT_ID('dbo.1050_CuadroAmortizacionFromID') IS NULL EXEC('CREATE FUNCTION [dbo].[1050_CuadroAmortizacionFromID](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1050_CuadroAmortizacionFromID_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_AfiliadoNombreXCI =====
IF OBJECT_ID('dbo.1100_AfiliadoNombreXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_AfiliadoNombreXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_AfiliadoNombreXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_FacturaRetenidaXNroFactura =====
IF OBJECT_ID('dbo.1100_FacturaRetenidaXNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_FacturaRetenidaXNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_FacturaRetenidaXNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_PagoErrorXIDFactura =====
IF OBJECT_ID('dbo.1100_PagoErrorXIDFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_PagoErrorXIDFactura](@pIDFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_PagoErrorXIDFactura_q](@pIDFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionAvisoXIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionAvisoXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionAvisoXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionAvisoXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionesXIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionesXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionesXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionesXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionItemFacturaXIDPrestamoFecha =====
IF OBJECT_ID('dbo.1100_RetencionItemFacturaXIDPrestamoFecha') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionItemFacturaXIDPrestamoFecha](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionItemFacturaXIDPrestamoFecha_q](@pIDPrestamo, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionItemXIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionItemXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionItemXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionItemXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionPagoXClave =====
IF OBJECT_ID('dbo.1100_RetencionPagoXClave') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionPagoXClave](@pIDPrestamo INT, @pFecha DATETIME2(0), @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionPagoXClave_q](@pIDPrestamo, @pFecha, @pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionPagoXIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionPagoXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionPagoXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionPagoXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionPrestamoXIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionPrestamoXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionPrestamoXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionPrestamoXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionxIDPrestamo =====
IF OBJECT_ID('dbo.1100_RetencionxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1100_RetencionXIDPrestamoFecha =====
IF OBJECT_ID('dbo.1100_RetencionXIDPrestamoFecha') IS NULL EXEC('CREATE FUNCTION [dbo].[1100_RetencionXIDPrestamoFecha](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1100_RetencionXIDPrestamoFecha_q](@pIDPrestamo, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1110_EmpresaPromedioXCI =====
IF OBJECT_ID('dbo.1110_EmpresaPromedioXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1110_EmpresaPromedioXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1110_EmpresaPromedioXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1110_FacturasVenciasxIDPrestamoFecha =====
IF OBJECT_ID('dbo.1110_FacturasVenciasxIDPrestamoFecha') IS NULL EXEC('CREATE FUNCTION [dbo].[1110_FacturasVenciasxIDPrestamoFecha](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1110_FacturasVenciasxIDPrestamoFecha_q](@pIDPrestamo, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1120_Retencion_Amort =====
IF OBJECT_ID('dbo.1120_Retencion_Amort') IS NULL EXEC('CREATE VIEW [dbo].[1120_Retencion_Amort] AS SELECT * FROM [dbo].[acc_sp_1120_Retencion_Amort_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1120_Retencion_Amort111 =====
IF OBJECT_ID('dbo.1120_Retencion_Amort111') IS NULL EXEC('CREATE VIEW [dbo].[1120_Retencion_Amort111] AS SELECT * FROM [dbo].[acc_sp_1120_Retencion_Amort111_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1120_Retencion_Tel =====
IF OBJECT_ID('dbo.1120_Retencion_Tel') IS NULL EXEC('CREATE VIEW [dbo].[1120_Retencion_Tel] AS SELECT * FROM [dbo].[acc_sp_1120_Retencion_Tel_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1130_FacturaInteresMes_Orig =====
IF OBJECT_ID('dbo.1130_FacturaInteresMes_Orig') IS NULL EXEC('CREATE FUNCTION [dbo].[1130_FacturaInteresMes_Orig](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1130_FacturaInteresMes_Orig_q](@pCodMoneda)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1140_FacturaDetallexIDPrestamoCodItemPago =====
IF OBJECT_ID('dbo.1140_FacturaDetallexIDPrestamoCodItemPago') IS NULL EXEC('CREATE FUNCTION [dbo].[1140_FacturaDetallexIDPrestamoCodItemPago](@pIDPrestamo INT, @pCodItemPago NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1140_FacturaDetallexIDPrestamoCodItemPago_q](@pIDPrestamo, @pCodItemPago)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1150_AfiliadoCometarioXCI =====
IF OBJECT_ID('dbo.1150_AfiliadoCometarioXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1150_AfiliadoCometarioXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1150_AfiliadoCometarioXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1200_CuotasPendientesXIDPrestamo =====
IF OBJECT_ID('dbo.1200_CuotasPendientesXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1200_CuotasPendientesXIDPrestamo](@pIDPrestamo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1200_CuotasPendientesXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1200_FacturasPendientesXIDPrestamo =====
IF OBJECT_ID('dbo.1200_FacturasPendientesXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1200_FacturasPendientesXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1200_FacturasPendientesXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1234_PagoParcialPorID =====
IF OBJECT_ID('dbo.1234_PagoParcialPorID') IS NULL EXEC('CREATE FUNCTION [dbo].[1234_PagoParcialPorID](@pIDPrestamo INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1234_PagoParcialPorID_q](@pIDPrestamo, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1234_PagosParcialesXIDPrestamo =====
IF OBJECT_ID('dbo.1234_PagosParcialesXIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1234_PagosParcialesXIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1234_PagosParcialesXIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_AfiliadoxCI =====
IF OBJECT_ID('dbo.2000_Rpt_AfiliadoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_AfiliadoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_AfiliadoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_AutorizacionxIDPrestamo =====
IF OBJECT_ID('dbo.2000_Rpt_AutorizacionxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_AutorizacionxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_AutorizacionxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 999_Parametros =====
IF OBJECT_ID('dbo.999_Parametros') IS NULL EXEC('CREATE FUNCTION [dbo].[999_Parametros](@pLogin NVARCHAR(MAX), @pClave NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_999_Parametros_q](@pLogin, @pClave)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Factura =====
IF OBJECT_ID('dbo.Rpt_Factura') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Factura] AS SELECT * FROM [dbo].[acc_sp_Rpt_Factura_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Factura_DBG =====
IF OBJECT_ID('dbo.Rpt_Factura_DBG') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Factura_DBG] AS SELECT * FROM [dbo].[acc_sp_Rpt_Factura_DBG_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Pago =====
IF OBJECT_ID('dbo.Rpt_Pago') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Pago] AS SELECT * FROM [dbo].[acc_sp_Rpt_Pago_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_PagoError =====
IF OBJECT_ID('dbo.Rpt_PagoError') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_PagoError] AS SELECT * FROM [dbo].[acc_sp_Rpt_PagoError_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_PrestamoCuadro =====
IF OBJECT_ID('dbo.Rpt_PrestamoCuadro') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_PrestamoCuadro] AS SELECT * FROM [dbo].[acc_sp_Rpt_PrestamoCuadro_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_RetencionPago =====
IF OBJECT_ID('dbo.Rpt_RetencionPago') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_RetencionPago] AS SELECT * FROM [dbo].[acc_sp_Rpt_RetencionPago_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_RetencionPrestamo =====
IF OBJECT_ID('dbo.Rpt_RetencionPrestamo') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_RetencionPrestamo] AS SELECT * FROM [dbo].[acc_sp_Rpt_RetencionPrestamo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoComentario =====
IF OBJECT_ID('dbo.Rs_AfiliadoComentario') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoComentario] AS SELECT * FROM [dbo].[acc_sp_Rs_AfiliadoComentario_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Empresa_Descrip =====
IF OBJECT_ID('dbo.Rs_Empresa_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Empresa_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_Empresa_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_FacturaEstado_Descrip =====
IF OBJECT_ID('dbo.Rs_FacturaEstado_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_FacturaEstado_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_FacturaEstado_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_FacturaTipo_Descrip =====
IF OBJECT_ID('dbo.Rs_FacturaTipo_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_FacturaTipo_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_FacturaTipo_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Moneda_Descrip =====
IF OBJECT_ID('dbo.Rs_Moneda_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Moneda_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_Moneda_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PagoOrigen_Descrip =====
IF OBJECT_ID('dbo.Rs_PagoOrigen_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PagoOrigen_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_PagoOrigen_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoActivo =====
IF OBJECT_ID('dbo.Rs_PrestamoActivo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoActivo] AS SELECT * FROM [dbo].[acc_sp_Rs_PrestamoActivo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoEstado_Descrip =====
IF OBJECT_ID('dbo.Rs_PrestamoEstado_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoEstado_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_PrestamoEstado_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoInteresAmortizacion =====
IF OBJECT_ID('dbo.Rs_PrestamoInteresAmortizacion') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoInteresAmortizacion] AS SELECT * FROM [dbo].[acc_sp_Rs_PrestamoInteresAmortizacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoPctRetenciones =====
IF OBJECT_ID('dbo.Rs_PrestamoPctRetenciones') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoPctRetenciones] AS SELECT * FROM [dbo].[acc_sp_Rs_PrestamoPctRetenciones_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoTipo_Descrip =====
IF OBJECT_ID('dbo.Rs_PrestamoTipo_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoTipo_Descrip] AS SELECT * FROM [dbo].[acc_sp_Rs_PrestamoTipo_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: wFactura =====
IF OBJECT_ID('dbo.wFactura') IS NULL EXEC('CREATE VIEW [dbo].[wFactura] AS SELECT * FROM [dbo].[acc_sp_wFactura_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: wRetencionDetalle =====
IF OBJECT_ID('dbo.wRetencionDetalle') IS NULL EXEC('CREATE VIEW [dbo].[wRetencionDetalle] AS SELECT * FROM [dbo].[acc_sp_wRetencionDetalle_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xw_FacturasCantidadXPrestamo =====
IF OBJECT_ID('dbo.xw_FacturasCantidadXPrestamo') IS NULL EXEC('CREATE VIEW [dbo].[xw_FacturasCantidadXPrestamo] AS SELECT * FROM [dbo].[acc_sp_xw_FacturasCantidadXPrestamo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xw_FacturasDeCancelacionAnticipada =====
IF OBJECT_ID('dbo.xw_FacturasDeCancelacionAnticipada') IS NULL EXEC('CREATE VIEW [dbo].[xw_FacturasDeCancelacionAnticipada] AS SELECT * FROM [dbo].[acc_sp_xw_FacturasDeCancelacionAnticipada_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1000_AfiliadoPromedioMeses =====
IF OBJECT_ID('dbo.1000_AfiliadoPromedioMeses') IS NULL EXEC('CREATE FUNCTION [dbo].[1000_AfiliadoPromedioMeses](@pCI INT, @pAnioMesIni INT, @pAnioMesFin INT, @pMeses NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1000_AfiliadoPromedioMeses_q](@pCI, @pAnioMesIni, @pAnioMesFin, @pMeses)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1009_PrestamoVale =====
IF OBJECT_ID('dbo.1009_PrestamoVale') IS NULL EXEC('CREATE FUNCTION [dbo].[1009_PrestamoVale](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1009_PrestamoVale_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Prestamo_Crystal =====
IF OBJECT_ID('dbo.Rpt_Prestamo_Crystal') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Prestamo_Crystal] AS SELECT * FROM [dbo].[acc_sp_Rpt_Prestamo_Crystal_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1025_TotalPagoParcialPorPrestamo =====
IF OBJECT_ID('dbo.1025_TotalPagoParcialPorPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[1025_TotalPagoParcialPorPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1025_TotalPagoParcialPorPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1030_FlujoTIR =====
IF OBJECT_ID('dbo.1030_FlujoTIR') IS NULL EXEC('CREATE VIEW [dbo].[1030_FlujoTIR] AS SELECT * FROM [dbo].[acc_sp_1030_FlujoTIR_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Retencion =====
IF OBJECT_ID('dbo.Rpt_Retencion') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Retencion] AS SELECT * FROM [dbo].[acc_sp_Rpt_Retencion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_FacturaxIDPrestamo =====
IF OBJECT_ID('dbo.2000_Rpt_FacturaxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_FacturaxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_FacturaxIDPrestamoEstado =====
IF OBJECT_ID('dbo.2000_Rpt_FacturaxIDPrestamoEstado') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_FacturaxIDPrestamoEstado](@pIDPrestamo INT, @pCodFacturaEstado NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamoEstado_q](@pIDPrestamo, @pCodFacturaEstado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_FacturaxNroFactura =====
IF OBJECT_ID('dbo.2000_Rpt_FacturaxNroFactura') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_FacturaxNroFactura](@pNroFactura INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_FacturaxNroFactura_q](@pNroFactura)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Tmp_Rpt_Factura =====
IF OBJECT_ID('dbo.500_Tmp_Rpt_Factura') IS NULL EXEC('CREATE VIEW [dbo].[500_Tmp_Rpt_Factura] AS SELECT * FROM [dbo].[acc_sp_500_Tmp_Rpt_Factura_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 2000_Rpt_PrestamoCuadroxIDPrestamo =====
IF OBJECT_ID('dbo.2000_Rpt_PrestamoCuadroxIDPrestamo') IS NULL EXEC('CREATE FUNCTION [dbo].[2000_Rpt_PrestamoCuadroxIDPrestamo](@pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_2000_Rpt_PrestamoCuadroxIDPrestamo_q](@pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1116_AfiliadoComentarioXCI =====
IF OBJECT_ID('dbo.1116_AfiliadoComentarioXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1116_AfiliadoComentarioXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1116_AfiliadoComentarioXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1022_ImporteCuotaPrestamoActivoXCI =====
IF OBJECT_ID('dbo.1022_ImporteCuotaPrestamoActivoXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1022_ImporteCuotaPrestamoActivoXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1022_ImporteCuotaPrestamoActivoXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1022_PrestamoActivoXCI =====
IF OBJECT_ID('dbo.1022_PrestamoActivoXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1022_PrestamoActivoXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1022_PrestamoActivoXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Prestamo =====
IF OBJECT_ID('dbo.Rpt_Prestamo') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Prestamo] AS SELECT * FROM [dbo].[acc_sp_Rpt_Prestamo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Prestamo2 =====
IF OBJECT_ID('dbo.Rpt_Prestamo2') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Prestamo2] AS SELECT * FROM [dbo].[acc_sp_Rpt_Prestamo2_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1115_PrestamosAnterioresxCI =====
IF OBJECT_ID('dbo.1115_PrestamosAnterioresxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[1115_PrestamosAnterioresxCI](@pCI INT, @pIDPrestamo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1115_PrestamosAnterioresxCI_q](@pCI, @pIDPrestamo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: wSaldoSum =====
IF OBJECT_ID('dbo.wSaldoSum') IS NULL EXEC('CREATE VIEW [dbo].[wSaldoSum] AS SELECT * FROM [dbo].[acc_sp_wSaldoSum_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xw_PrestamosAnticipados =====
IF OBJECT_ID('dbo.xw_PrestamosAnticipados') IS NULL EXEC('CREATE VIEW [dbo].[xw_PrestamosAnticipados] AS SELECT * FROM [dbo].[acc_sp_xw_PrestamosAnticipados_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Buscar duplicados por 1030_FlujoTIR =====
IF OBJECT_ID('dbo.Buscar duplicados por 1030_FlujoTIR') IS NULL EXEC('CREATE VIEW [dbo].[Buscar duplicados por 1030_FlujoTIR] AS SELECT * FROM [dbo].[acc_sp_Buscar_duplicados_por_1030_FlujoTIR_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 1130_FacturaInteresMes =====
IF OBJECT_ID('dbo.1130_FacturaInteresMes') IS NULL EXEC('CREATE FUNCTION [dbo].[1130_FacturaInteresMes](@pCodMoneda NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sp_1130_FacturaInteresMes_q](@pCodMoneda)
)')
GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1000_AfiladoCI2Nombre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1000_AfiladoCI2Nombre]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Afiliado.CI AS CI, [SP_Afiliado].[Nombres] + ' ' + [SP_Afiliado].[Apellido1] + (CASE WHEN [SP_Afiliado].[Apellido2] + ''<>'' THEN ' ' ELSE '' END) + [SP_Afiliado].[Apellido2] AS DescAfiliado, SP_Afiliado.Direccion, SP_Afiliado.Telefono
    FROM SP_Afiliado
    WHERE (((SP_Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1000_AfiliadoImpLiquidoxMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1000_AfiliadoImpLiquidoxMes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_ImpLiquido.CI, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, Sum(SP_ImpLiquido.Importe) AS Importe, MIN(SP_ImpLiquido.AnioMes) AS AnioMes
    FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)
    WHERE (((SP_Trabaja.FechaBaja) Is Null Or (SP_Trabaja.FechaBaja)>CAST(GETDATE() AS date)) AND ((SP_Trabaja.FechaIngreso)<=CAST(GETDATE() AS date)))
    GROUP BY SP_ImpLiquido.CI, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1000_Borrar_CuadroAmortizacion_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1000_Borrar_CuadroAmortizacion_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM SP_CuadroAmortizacion_Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1000_PrestamoxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1000_PrestamoxIDPrestamo]
    @pIdPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.*
    FROM SP_Prestamo
    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1001_AfiliadoImpLiquidoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1001_AfiliadoImpLiquidoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_ImpLiquido.CI, SP_ImpLiquido.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, SP_ImpLiquido.Importe, SP_ImpLiquido.AnioMes
    FROM (SP_Empresa INNER JOIN SP_ImpLiquido ON SP_Empresa.CodEmpresa = SP_ImpLiquido.CodEmpresa) INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CI = SP_Trabaja.CI) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa)
    WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null Or (SP_Trabaja.FechaBaja)>CAST(GETDATE() AS date)))
    ORDER BY SP_ImpLiquido.CodEmpresa, SP_ImpLiquido.Anio DESC , SP_ImpLiquido.Mes DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1001_FacturaIdMax =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1001_FacturaIdMax]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(SP_Factura.IdFactura) AS Max
    FROM SP_Factura;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1001_FacturaMax =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1001_FacturaMax]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(SP_Factura.NroFactura) AS Max
    FROM SP_Factura;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1001_PrestamoMax =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1001_PrestamoMax]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(SP_Prestamo.IdPrestamo) AS Max
    FROM SP_Prestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_ActualizarPrestamoEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_ActualizarPrestamoEstado]
    @pIdPrestamo INT,
    @pCodPrestamoEstado NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Prestamo SET SP_Prestamo.CodPrestamoEstado = @pCodPrestamoEstado, SP_Prestamo.Usr = @pUsr, SP_Prestamo.Ts = SYSDATETIME()
    WHERE (((SP_Prestamo.IDPrestamo)=@pIdPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_ActualizarPrestamoxCuota =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_ActualizarPrestamoxCuota]
    @pIdPrestamo INT,
    @pImporte NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    IF COL_LENGTH('dbo.SP_Prestamo','CuotasPagas') IS NOT NULL AND COL_LENGTH('dbo.SP_Prestamo','Saldo') IS NOT NULL
    BEGIN
        EXEC sp_executesql N'UPDATE SP_Prestamo SET CuotasPagas = CuotasPagas + 1, Saldo = Saldo - @pImporte WHERE IdPrestamo = @pIdPrestamo', N'@pImporte NVARCHAR(MAX), @pIdPrestamo INT', @pImporte=@pImporte, @pIdPrestamo=@pIdPrestamo;
    END
    ELSE
    BEGIN
        SELECT 1 AS NoOp;
    END
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_ActualizarTasaCambio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_ActualizarTasaCambio]
    @pCodMoneda NVARCHAR(MAX),
    @pTasaCambio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Moneda SET SP_Moneda.TasaCambio = @pTasaCambio
    WHERE (((SP_Moneda.CodMoneda)=@pCodMoneda));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_CuadroAmortizacionxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.*
    FROM SP_CuadroAmortizacion
    WHERE (((SP_CuadroAmortizacion.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_CuadroAmortizacionxIDPrestamoNroCuota =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_CuadroAmortizacionxIDPrestamoNroCuota]
    @pIdPrestamo INT,
    @pNroCuota NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.IDPrestamo, SP_CuadroAmortizacion.NroCuota, SP_CuadroAmortizacion.Monto, SP_CuadroAmortizacion.ImporteCuota, SP_CuadroAmortizacion.Interes, SP_CuadroAmortizacion.Amortizacion, SP_CuadroAmortizacion.Saldo, SP_CuadroAmortizacion.Usr, SP_CuadroAmortizacion.Ts
    FROM SP_CuadroAmortizacion
    WHERE (((SP_CuadroAmortizacion.IDPrestamo)=@pIdPrestamo) AND ((SP_CuadroAmortizacion.NroCuota)=@pNroCuota));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_CuotasxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_CuotasxIDPrestamo]
    @pIdPrestamo NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IdPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_CuotaEstado.Descrip AS DescCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
    FROM SP_Cuota INNER JOIN SP_CuotaEstado ON SP_Cuota.CodCuotaEstado = SP_CuotaEstado.CodCuotaEstado
    WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_CuotasxIDPrestamoCuotaEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_CuotasxIDPrestamoCuotaEstado]
    @pIdPrestamo INT,
    @pCodCuotaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.*
    FROM SP_Cuota
    WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_CuotaxIDPrestamoNro =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_CuotaxIDPrestamoNro]
    @pIdPrestamo INT,
    @pNro NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.*
    FROM SP_Cuota
    WHERE (((SP_Cuota.IdPrestamo)=@pIdPrestamo) AND ((SP_Cuota.Nro)=@pNro));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturaDetallexIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturaDetallexIDFactura]
    @pIdFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_FacturaDetalle.*
    FROM SP_FacturaDetalle
    WHERE (((SP_FacturaDetalle.IdFactura)=@pIdFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturaDetallexIDFacturaCodItemPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturaDetallexIDFacturaCodItemPago]
    @pIdFactura INT,
    @pCodItemPago NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_FacturaDetalle.*
    FROM SP_FacturaDetalle
    WHERE (((SP_FacturaDetalle.IdFactura)=@pIdFactura) AND ((SP_FacturaDetalle.CodItemPago)=@pCodItemPago));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturasEmitidasXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturasEmitidasXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts
    FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)='emi'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturasxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturasxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.CodFacturaTipo
    FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
    ORDER BY SP_Factura.IDFactura;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturaxIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturaxIDFactura]
    @pIDFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.*
    FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
    WHERE (((SP_Factura.IDFactura)=@pIDFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_FacturaxNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_FacturaxNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.Usr, SP_Factura.Ts
    FROM SP_Factura INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
    WHERE (((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_LiquidoAnioMesxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_LiquidoAnioMesxCI]
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT SP_ImpLiquido.AnioMes AS AnioMes
    FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)
    WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null) AND ((SP_ImpLiquido.AnioMes) Between @pMesIni And @pMesFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_PagoItemPagoxIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_PagoItemPagoxIDFactura]
    @pIdFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Pago_ItemPago.*
    FROM SP_Pago_ItemPago
    WHERE (((SP_Pago_ItemPago.IdFactura)=@pIdFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_PagoxIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_PagoxIDFactura]
    @pIdFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Pago.*
    FROM SP_Pago
    WHERE (((SP_Pago.IdFactura)=@pIdFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_PrestamoAbiertoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_PrestamoAbiertoxCI]
    @pCI INT,
    @pCodPrestamoTipo NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.*
    FROM SP_Prestamo
    WHERE (((SP_Prestamo.CI)=@pCI) AND ((SP_Prestamo.CodPrestamoEstado) NOT In ('anu','can','car')) AND ((SP_Prestamo.CodPrestamoTipo)=@pCodPrestamoTipo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_PrestamoxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_PrestamoxIDPrestamo]
    @pIdPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.*
    FROM SP_Prestamo
    WHERE (((SP_Prestamo.IdPrestamo)=@pIdPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1002_TasasxCodMoneda =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1002_TasasxCodMoneda]
    @pCodMoneda NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Moneda.TasaCambio, SP_Moneda.TasaMora, SP_Moneda.Tasa, SP_Moneda.CodAbitab
    FROM SP_Moneda
    WHERE (((SP_Moneda.CodMoneda)=@pCodMoneda));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_CuotasPendientes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_CuotasPendientes]
    @pIdPrestamo INT,
    @pCodCuotaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_CuotaEstado.Descrip AS DescCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
    FROM SP_Cuota INNER JOIN SP_CuotaEstado ON SP_Cuota.CodCuotaEstado = SP_CuotaEstado.CodCuotaEstado
    WHERE (((SP_Cuota.IDPrestamo)=@pIdPrestamo) AND ((SP_Cuota.FechaVencimiento)<CAST(GETDATE() AS date)) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_CuotaxIDPrestamoNro =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_CuotaxIDPrestamoNro]
    @pIDPrestamo INT,
    @pNro NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
    FROM SP_Cuota
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.Nro)=@pNro));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_CuotaxNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_CuotaxNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.Importe, SP_Cuota.CodCuotaEstado, SP_Cuota.FechaPago, SP_Cuota.IDPrestamo, SP_Cuota.Nro
    FROM (SP_FacturaDetalle INNER JOIN SP_Factura ON SP_FacturaDetalle.IdFactura = SP_Factura.IDFactura) INNER JOIN SP_Cuota ON (SP_FacturaDetalle.NroCuota = SP_Cuota.Nro) AND (SP_Factura.IdPrestamo = SP_Cuota.IDPrestamo)
    WHERE (((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_FacturasxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_FacturasxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.CodMoneda, SP_Moneda.Descrip AS Moneda, SP_Factura.Importe, SP_Factura.IDFactura, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.FechaPago
    FROM (SP_Factura INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo))
    ORDER BY SP_Factura.NroFactura;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_FacturasxIDPrestamoEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_FacturasxIDPrestamoEstado]
    @pIDPrestamo INT,
    @pCodFacturaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.IdPrestamo, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.CodFacturaTipo
    FROM SP_Factura INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)=@pCodFacturaEstado));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_PagosSingleXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_PagosSingleXIDPrestamo]
    @pIdPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Pago.*
    FROM SP_Pago
    WHERE IdFactura in (select IdFactura from SP_Factura where IdPrestamo=@pIdPrestamo);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_PagosxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_PagosxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Pago.Fecha, SP_Pago.Importe, SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Factura.Usr, SP_Factura.Ts
    FROM (SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_PagoxNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_PagoxNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Pago.*
    FROM SP_Pago INNER JOIN SP_Factura ON SP_Pago.IdFactura = SP_Factura.IdFactura
    WHERE (((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1003_SaldoxNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1003_SaldoxNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.Saldo
    FROM (SP_FacturaDetalle INNER JOIN SP_Factura ON SP_FacturaDetalle.IdFactura = SP_Factura.IdFactura) INNER JOIN SP_CuadroAmortizacion ON (SP_Factura.IdPrestamo = SP_CuadroAmortizacion.IdPrestamo) AND (SP_FacturaDetalle.NroCuota = SP_CuadroAmortizacion.NroCuota)
    WHERE (((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1004_ActualizarCuotaEstadoxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1004_ActualizarCuotaEstadoxIDPrestamo]
    @pIDPrestamo INT,
    @pCodCuotaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Cuota SET SP_Cuota.CodCuotaEstado = @pCodCuotaEstado
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1004_ActualizarFacturaEstadoxIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1004_ActualizarFacturaEstadoxIDFactura]
    @pIDFactura INT,
    @pCodFacturaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaEstado = @pCodFacturaEstado
    WHERE (((SP_Factura.IDFactura)=@pIDFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1004_ActualizarFacturaEstadoxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1004_ActualizarFacturaEstadoxIDPrestamo]
    @pIDPrestamo INT,
    @pCodFacturaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaEstado = @pCodFacturaEstado
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1004_ActualizarPrestamoEstadoxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1004_ActualizarPrestamoEstadoxIDPrestamo]
    @pIDPrestamo INT,
    @pCodPrestamoEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Prestamo SET SP_Prestamo.CodPrestamoEstado = @pCodPrestamoEstado
    WHERE (((SP_Prestamo.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1005_ActualizarFacturaCancelarxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1005_ActualizarFacturaCancelarxIDPrestamo]
    @pIDPrestamo INT,
    @pCodFacturaEstadoActual NVARCHAR(MAX),
    @pCodFacturaEstadoNuevo NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaEstado = @pCodFacturaEstadoNuevo, SP_Factura.Usr = @pUsr, SP_Factura.FechaPago = SYSDATETIME()
    WHERE (((SP_Factura.CodFacturaEstado)=@pCodFacturaEstadoActual) AND ((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1006_Borrar_rptPlanes_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1006_Borrar_rptPlanes_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM rptPlanes_Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1007_Borrar_ImpLiquido =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1007_Borrar_ImpLiquido]
    @pCI INT,
    @pCodEmpresa NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE SP_ImpLiquido FROM SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)
    WHERE (((SP_Trabaja.FechaBaja) Is Null) AND ((SP_ImpLiquido.CI)=@pCI) AND ((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa) AND ((SP_ImpLiquido.Mes)=@pMes) AND ((SP_ImpLiquido.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1007_ImpLiquidoxCICodEmpresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1007_ImpLiquidoxCICodEmpresa]
    @pCI INT,
    @pCodEmpresa NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_ImpLiquido.CI, SP_ImpLiquido.CodEmpresa, SP_ImpLiquido.Anio, SP_ImpLiquido.Mes, SP_ImpLiquido.Importe
    FROM SP_ImpLiquido
    WHERE (((SP_ImpLiquido.CI)=@pCI) AND ((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa))
    ORDER BY SP_ImpLiquido.Anio DESC , SP_ImpLiquido.Mes DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1007_Insert_ImpLiquido =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1007_Insert_ImpLiquido]
    @pCI INT,
    @pCodEmpresa NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pImporte NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SP_ImpLiquido ( CI, CodEmpresa, Fechaingreso, IdTrabaja, Mes, Anio, AnioMes, Importe, Usr, Ts )
    SELECT SP_Trabaja.CI, SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, @pMes AS Expr1, @pAnio AS Expr2, ((TRY_CONVERT(int,@pAnio) * 100) + TRY_CONVERT(int,@pMes)) AS Expr3, @pImporte AS Expr6, @pUsr AS Expr4, SYSDATETIME() AS Expr5
    FROM SP_Trabaja
    WHERE (((SP_Trabaja.CI)=@pCI) AND ((SP_Trabaja.CodEmpresa)=@pCodEmpresa));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1007_TrabajaxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1007_TrabajaxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Trabaja.CI, SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, SP_Empresa.Nombre AS DescEmpresa
    FROM SP_Trabaja INNER JOIN SP_Empresa ON SP_Trabaja.CodEmpresa = SP_Empresa.CodEmpresa
    WHERE (((SP_Trabaja.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1008_Borrar_rptCheque_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1008_Borrar_rptCheque_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM rptCheque_Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1009_Borrar_rptVale_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1009_Borrar_rptVale_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM rptVale_Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1009_MinFechaVencimiento =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1009_MinFechaVencimiento]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IDPrestamo, Min(SP_Cuota.FechaVencimiento) AS FechaVencimiento
    FROM SP_Cuota
    GROUP BY SP_Cuota.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1009_PrestamoCesion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1009_PrestamoCesion]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Cuotas, SP_Prestamo.ImporteCuota, SP_Prestamo.Tasa, SP_Prestamo.Importe
    FROM (SP_Prestamo INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI
    WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1010_CtrlPrestamoEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1010_CtrlPrestamoEstado]
    @pCodPrestamoEstadoSig NVARCHAR(MAX),
    @pCodPrestamoEstadoAnt NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CtrlPrestamoEstado.PrestamoEstadoSig, SP_CtrlPrestamoEstado.PrestamoEstadoAnt
    FROM SP_CtrlPrestamoEstado
    WHERE (((SP_CtrlPrestamoEstado.PrestamoEstadoSig)=@pCodPrestamoEstadoSig) AND ((SP_CtrlPrestamoEstado.PrestamoEstadoAnt)=@pCodPrestamoEstadoAnt));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1011_Prestamo_MinFechaVencimiento =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1011_Prestamo_MinFechaVencimiento]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IDPrestamo, Min(SP_Cuota.FechaVencimiento) AS FechaVencimiento
    FROM SP_Cuota
    WHERE (((SP_Cuota.CodCuotaEstado)<>'anu'))
    GROUP BY SP_Cuota.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1015_Borrar_ImpLiquidoxEmpresaAnioMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1015_Borrar_ImpLiquidoxEmpresaAnioMes]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM SP_ImpLiquido
    WHERE (((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa) AND ((SP_ImpLiquido.Mes)=@pMes) AND ((SP_ImpLiquido.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1015_CargaLiquidos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1015_CargaLiquidos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CargaLiquidos.cedula*10+ CargaLiquidos.chkdig AS CI, CargaLiquidos.imphaberes- CargaLiquidos.impdescuen AS Importe
    FROM CargaLiquidos;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1015_ImpLiquidoxEmpresaAnioMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1015_ImpLiquidoxEmpresaAnioMes]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_ImpLiquido.*
    FROM SP_ImpLiquido
    WHERE (((SP_ImpLiquido.CodEmpresa)=@pCodEmpresa) AND ((SP_ImpLiquido.Mes)=@pMes) AND ((SP_ImpLiquido.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1015_TrabajaxMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1015_TrabajaxMes]
    @pCI INT,
    @pCodEmpresa NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pMes NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, SP_Trabaja.CodEmpresa
    FROM SP_Trabaja
    WHERE (YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])) <= TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AND (SP_Trabaja.FechaBaja Is Null OR (YEAR([SP_Trabaja].[FechaBaja]) * 100 + MONTH([SP_Trabaja].[FechaBaja])) > TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2))) AND SP_Trabaja.CI = @pCI 
    AND (YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])) <= TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) 
    AND SP_Trabaja.CodEmpresa = @pCodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1016_AfiliadoCINombre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1016_AfiliadoCINombre]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2
    FROM SP_Afiliado;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1017_LiquidoSubsidioxMesAnio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1017_LiquidoSubsidioxMesAnio]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioCabezal.ImpLiquido) AS ImpLiquido
    FROM SubsidioCabezal
    WHERE (((SubsidioCabezal.Liquidar)= 1))
    GROUP BY SubsidioCabezal.CI, SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1018_ActualizarFacturaImpresaxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1018_ActualizarFacturaImpresaxIDPrestamo]
    @pIDPrestamo INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.Impresiones = ISNULL(SP_Factura.Impresiones, 0) + 1, SP_Factura.Usr = @pUsr, SP_Factura.Ts = SYSDATETIME()
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1018_ActualizarFacturaImpresaxNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1018_ActualizarFacturaImpresaxNroFactura]
    @pNroFactura INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.Impresiones = ISNULL(SP_Factura.Impresiones, 0) + 1, SP_Factura.Usr = @pUsr, SP_Factura.Ts = SYSDATETIME()
    WHERE (((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_BorrarCuotasxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_BorrarCuotasxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM SP_Cuota
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_BorrarFacturasxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_BorrarFacturasxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM SP_Factura
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_CargarLiquidosxMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_CargarLiquidosxMes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT C.CEDULA AS CI, Sum(C.LIQUIDO) AS Importe
    FROM CargaLiquidos AS C
    GROUP BY C.CEDULA;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_Insert_ImpLiquido =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_Insert_ImpLiquido]
    @pCI INT,
    @pCodEmpresa INT,
    @pAnioMes INT,
    @pImporte NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SP_ImpLiquido ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Usr, Ts, IdTrabaja, Importe, AnioMes )
    SELECT SP_Trabaja.CI, SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, TRY_CONVERT(float,SUBSTRING(CONVERT(nvarchar(max),@pAnioMes),5,2)) AS Expr1, TRY_CONVERT(float,SUBSTRING(CONVERT(nvarchar(max),@pAnioMes),1,4)) AS Expr2, @pUsr AS Expr3, SYSDATETIME() AS Expr4, SP_Trabaja.IdTrabaja, @pImporte AS Expr5, @pAnioMes AS Expr6
    FROM SP_Trabaja
    WHERE (SP_Trabaja.CI=@pCI AND SP_Trabaja.CodEmpresa=@pCodEmpresa AND (YEAR(SP_Trabaja.FechaIngreso) * 100 + MONTH(SP_Trabaja.FechaIngreso))<=@pAnioMes AND (SP_Trabaja.FechaBaja IS NULL OR (YEAR(SP_Trabaja.FechaBaja) * 100 + MONTH(SP_Trabaja.FechaBaja))>@pAnioMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_UItVtoCuotaxIDPrestamoEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_UItVtoCuotaxIDPrestamoEstado]
    @pIDPrestamo INT,
    @pCodCuotaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(SP_Cuota.FechaVencimiento) AS FechaVencimiento
    FROM SP_Cuota
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)=@pCodCuotaEstado));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1019_UltVtoCuotaNoPendienteXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1019_UltVtoCuotaNoPendienteXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(SP_Cuota.FechaVencimiento) AS FechaVencimiento
    FROM SP_Cuota
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)<>'pen'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1020_BorrarCargaxFechaNroReng =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1020_BorrarCargaxFechaNroReng]
    @pFecha DATETIME2(0),
    @pNroReng INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ErrCargaAbitab
    WHERE (((ErrCargaAbitab.Fecha)=@pFecha) AND ((ErrCargaAbitab.NroReng)=@pNroReng));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1025_PagoParcialFromPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1025_PagoParcialFromPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoParcial.*
    FROM SP_PagoParcial
    WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1026_PagosYFacturasXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1026_PagosYFacturasXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Pago.Fecha, SP_Pago.Importe, SP_Factura.Importe AS ImporteFactura, SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Factura.Usr, SP_Factura.Ts
    FROM (SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1030_Borrar_rptTIR =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1030_Borrar_rptTIR]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM rptTIR_Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: acc_sp_1030_FacturaFlujo_q =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_acc_sp_1030_FacturaFlujo_q]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, (YEAR(SP_Factura.FechaVencimiento) * 100 + MONTH(SP_Factura.FechaVencimiento)) AS Mes, SP_Factura.Importe
    FROM SP_Factura
    WHERE (((SP_Factura.CodFacturaEstado)<>'anu'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1030_PrestamoFlujo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1030_PrestamoFlujo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, (YEAR(SP_Prestamo.FechaCobro) * 100 + MONTH(SP_Prestamo.FechaCobro)) AS Mes, -SP_Prestamo.Importe AS Importe
    FROM SP_Prestamo
    WHERE (((SP_Prestamo.CodPrestamoEstado)<>'anu') AND ((SP_Prestamo.FechaCobro) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1050_CuadroAmortizacionFromID =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1050_CuadroAmortizacionFromID]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.*
    FROM SP_CuadroAmortizacion
    WHERE (((SP_CuadroAmortizacion.IDPrestamo)=@pIDPrestamo))
    ORDER BY SP_CuadroAmortizacion.NroCuota;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_AfiliadoNombreXCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_AfiliadoNombreXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Afiliado.CI, [SP_Afiliado].[Apellido1] + (CASE WHEN [SP_Afiliado].[Apellido2] + ''<>'' THEN ' ' + [SP_Afiliado].[Apellido2] ELSE '' END) + ', ' + [SP_Afiliado].[Nombres] AS DescAfiliado
    FROM SP_Afiliado
    WHERE (((SP_Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_FacturaRetenidaXNroFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_FacturaRetenidaXNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionItem.IDFactura, SP_RetencionItem.CodRetencionItemCod
    FROM SP_RetencionItem INNER JOIN SP_Factura ON SP_RetencionItem.IDFactura = SP_Factura.IDFactura
    WHERE (((SP_RetencionItem.CodRetencionItemCod)='fac') AND ((SP_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_PagoErrorXIDFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_PagoErrorXIDFactura]
    @pIDFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoError.*
    FROM SP_PagoError
    WHERE (((SP_PagoError.IDFactura)=@pIDFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionAvisoXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionAvisoXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionAviso.IDPrestamo, SP_RetencionAviso.Fecha, SP_RetencionAviso.Comentario, SP_RetencionAviso.Usr, SP_RetencionAviso.Ts
    FROM SP_RetencionAviso
    WHERE (((SP_RetencionAviso.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionesXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionesXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.IDPrestamo, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_Retencion.Importe AS Total, SP_RetencionItemCod.Descrip AS DescRetencionItemCod, SP_RetencionItem.IDFactura, SP_RetencionItem.Importe, SP_Retencion.Observaciones
    FROM SP_Retencion INNER JOIN (SP_RetencionItem INNER JOIN SP_RetencionItemCod ON SP_RetencionItem.CodRetencionItemCod = SP_RetencionItemCod.CodRetencionItemCod) ON (SP_Retencion.IDPrestamo = SP_RetencionItem.IDPrestamo) AND (SP_Retencion.Fecha = SP_RetencionItem.Fecha)
    WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionItemFacturaXIDPrestamoFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionItemFacturaXIDPrestamoFecha]
    @pIDPrestamo INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionItem.*
    FROM SP_RetencionItem
    WHERE (((SP_RetencionItem.IDPrestamo)=@pIDPrestamo) AND ((SP_RetencionItem.Fecha)=@pFecha) AND ((SP_RetencionItem.CodRetencionItemCod)='fac'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionItemXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionItemXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionItem.*
    FROM SP_RetencionItem
    WHERE (((SP_RetencionItem.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionPagoXClave =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionPagoXClave]
    @pIDPrestamo INT,
    @pFecha DATETIME2(0),
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPago.*
    FROM SP_RetencionPago
    WHERE (((SP_RetencionPago.IDPrestamo)=@pIDPrestamo) AND ((SP_RetencionPago.Fecha)=@pFecha) AND ((SP_RetencionPago.Mes)=@pMes) AND ((SP_RetencionPago.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionPagoXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionPagoXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPago.*
    FROM SP_RetencionPago
    WHERE (((SP_RetencionPago.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionPrestamoXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionPrestamoXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPrestamo.*
    FROM SP_RetencionPrestamo
    WHERE (((SP_RetencionPrestamo.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.*
    FROM SP_Retencion
    WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1100_RetencionXIDPrestamoFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1100_RetencionXIDPrestamoFecha]
    @pIDPrestamo INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.*
    FROM SP_Retencion
    WHERE (((SP_Retencion.IDPrestamo)=@pIDPrestamo) AND ((SP_Retencion.Fecha)=@pFecha));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1110_EmpresaPromedioXCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1110_EmpresaPromedioXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_ImpLiquido.CodEmpresa, MIN(SP_Empresa.Nombre) + ' - ' + FORMAT(Avg(SP_ImpLiquido.Importe),'0.00') AS Descrip
    FROM (SP_ImpLiquido INNER JOIN SP_Trabaja ON (SP_ImpLiquido.Fechaingreso = SP_Trabaja.FechaIngreso) AND (SP_ImpLiquido.CodEmpresa = SP_Trabaja.CodEmpresa) AND (SP_ImpLiquido.CI = SP_Trabaja.CI)) INNER JOIN SP_Empresa ON SP_ImpLiquido.CodEmpresa = SP_Empresa.CodEmpresa
    WHERE (((SP_ImpLiquido.AnioMes) Between (YEAR(DATEADD(month,-5,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-5,CAST(GETDATE() AS date)))) And (YEAR(DATEADD(month,-2,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-2,CAST(GETDATE() AS date))))) AND ((SP_ImpLiquido.CI)=@pCI) AND ((SP_Trabaja.FechaBaja) Is Null))
    GROUP BY SP_ImpLiquido.CodEmpresa
    ORDER BY Avg(SP_ImpLiquido.Importe) DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1110_FacturasVenciasxIDPrestamoFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1110_FacturasVenciasxIDPrestamoFecha]
    @pIDPrestamo INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.NroFactura, SP_Factura.FechaVencimiento, SP_Factura.Importe
    FROM SP_Factura
    WHERE (((SP_Factura.FechaVencimiento)<@pFecha) AND ((SP_Factura.FechaPago) Is Null) AND ((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)<>'anu'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1120_Retencion_Amort =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1120_Retencion_Amort]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha, Sum(SP_Factura.Importe*SP_Retencion.TipoCambio) AS Importe, Sum(SP_Factura.ImpAmortizable*SP_Retencion.TipoCambio) AS ImpAmortizable, Sum(SP_Factura.ImpInteres*SP_Retencion.TipoCambio) AS ImpInteres, Sum(SP_Pago.Importe*SP_Retencion.TipoCambio)-Sum(SP_Factura.Importe*SP_Retencion.TipoCambio) AS ImpMora
    FROM (SP_Retencion INNER JOIN ((SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_RetencionItem ON SP_Pago.IDFactura = SP_RetencionItem.IDFactura) ON (SP_Retencion.IDPrestamo = SP_RetencionItem.IDPrestamo) AND (SP_Retencion.Fecha = SP_RetencionItem.Fecha)) INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo
    GROUP BY SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1120_Retencion_Amort111 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1120_Retencion_Amort111]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.IDPrestamo, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CI, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_Retencion.Importe, SP_Retencion.Observaciones, SP_Retencion.Usr, SP_Retencion.Ts
    FROM (SP_Retencion INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1120_Retencion_Tel =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1120_Retencion_Tel]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha, Sum(SP_RetencionItem.Importe) AS Importe
    FROM SP_RetencionItem
    WHERE (((SP_RetencionItem.CodRetencionItemCod)='tel'))
    GROUP BY SP_RetencionItem.IDPrestamo, SP_RetencionItem.Fecha;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1130_FacturaInteresMes_Orig =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1130_FacturaInteresMes_Orig]
    @pCodMoneda NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, TRY_CONVERT(datetime2,'01/' + FORMAT(SP_Factura.FechaVencimiento,'mm/yy')) AS Mes, SP_Factura.ImpInteres
    FROM SP_Factura
    WHERE (((SP_Factura.CodFacturaEstado)<>'anu') AND ((SP_Factura.CodMoneda)=@pCodMoneda));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1140_FacturaDetallexIDPrestamoCodItemPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1140_FacturaDetallexIDPrestamoCodItemPago]
    @pIDPrestamo INT,
    @pCodItemPago NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, SP_FacturaDetalle.CodItemPago
    FROM SP_Factura INNER JOIN SP_FacturaDetalle ON SP_Factura.IDFactura = SP_FacturaDetalle.IdFactura
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_FacturaDetalle.CodItemPago)=@pCodItemPago));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1150_AfiliadoCometarioXCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1150_AfiliadoCometarioXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_AfiliadoComentario.*
    FROM SP_AfiliadoComentario
    WHERE (((SP_AfiliadoComentario.CI)=@pCI))
    ORDER BY SP_AfiliadoComentario.Fecha;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1200_CuotasPendientesXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1200_CuotasPendientesXIDPrestamo]
    @pIDPrestamo NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Cuota.IDPrestamo, SP_Cuota.Nro, SP_Cuota.FechaVencimiento, SP_Cuota.FechaPago, SP_Cuota.CodItemPago, SP_Cuota.Importe, SP_Cuota.CodMoneda, SP_Cuota.CodCuotaEstado, SP_Cuota.Usr, SP_Cuota.Ts
    FROM SP_Cuota
    WHERE (((SP_Cuota.IDPrestamo)=@pIDPrestamo) AND ((SP_Cuota.CodCuotaEstado)='pen'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1200_FacturasPendientesXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1200_FacturasPendientesXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, SP_Factura.IDFactura, SP_Factura.NroFactura, SP_Factura.NroEmpresa, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Factura.CodMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_Factura.TasaCambio, SP_Factura.CodigoBarra, SP_Factura.Impresiones, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.Usr, SP_Factura.Ts
    FROM SP_Factura
    WHERE (((SP_Factura.IdPrestamo)=@pIDPrestamo) AND ((SP_Factura.CodFacturaEstado)='emi'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1234_PagoParcialPorID =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1234_PagoParcialPorID]
    @pIDPrestamo INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoParcial.*
    FROM SP_PagoParcial
    WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo) AND ((SP_PagoParcial.Fecha)=@pFecha));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1234_PagosParcialesXIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1234_PagosParcialesXIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoParcial.*
    FROM SP_PagoParcial
    WHERE (((SP_PagoParcial.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_AfiliadoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_AfiliadoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Afiliado.Telefono
    FROM SP_Afiliado
    WHERE (((SP_Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_AutorizacionxIDPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_AutorizacionxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2
    FROM SP_Afiliado INNER JOIN SP_Prestamo ON SP_Afiliado.CI = SP_Prestamo.CI
    WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 999_Parametros =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_999_Parametros]
    @pLogin NVARCHAR(MAX),
    @pClave NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT xPar.login, xPar.Clave, xPar.orden, xPar.value1, xPar.value2, xPar.value3, xPar.value4, xPar.value5
    FROM xUsrParam AS xPar
    WHERE (((xPar.login)=@pLogin) AND ((xPar.Clave)=@pClave))
    ORDER BY xPar.orden;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 999_Parametros_Delete =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_999_Parametros_Delete]
    @pLogin NVARCHAR(MAX),
    @pClave NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE xPar FROM xUsrParam AS xPar
    WHERE (((xPar.login)=@pLogin) AND ((xPar.Clave)=@pClave));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 999_Parametros_Insert =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_999_Parametros_Insert]
    @pLogin NVARCHAR(MAX),
    @pClave NVARCHAR(MAX),
    @pOrden INT,
    @pValue1 NVARCHAR(MAX),
    @pValue2 NVARCHAR(MAX),
    @pValue3 NVARCHAR(MAX),
    @pValue4 NVARCHAR(MAX),
    @pValue5 NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO xUsrParam ( login, clave, orden, value1, value2, value3, value4, value5 )
    SELECT @pLogin, @pClave, @pOrden, @pValue1, @pValue2, @pValue3, @pValue4, @pValue5;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Factura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Factura]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Prestamo.Tasa, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_FacturaDetalle.Descrip, SP_FacturaDetalle.Importe, SP_FacturaDetalle.NroCuota, SP_Factura.Importe, SP_Factura.CodigoBarra, SP_Factura.CodFacturaEstado, SP_Factura.Impresiones, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.CodFacturaTipo, SP_FacturaTipo.Descrip AS DescFacturaTipo
    FROM ((((SP_Afiliado INNER JOIN SP_Prestamo ON SP_Afiliado.CI = SP_Prestamo.CI) INNER JOIN SP_Factura ON SP_Prestamo.IDPrestamo = SP_Factura.IdPrestamo) INNER JOIN SP_FacturaDetalle ON SP_Factura.IDFactura = SP_FacturaDetalle.IdFactura) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaTipo ON SP_Factura.CodFacturaTipo = SP_FacturaTipo.CodFacturaTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Factura_DBG =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Factura_DBG]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Factura.NroFactura, SP_Factura.FechaEmitida, SP_Factura.FechaVencimiento, SP_Factura.FechaPago, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Afiliado.Telefono, SP_Afiliado.EMail, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.Importe, SP_Factura.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_Factura.Usr, SP_Factura.Ts, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Factura.CodFacturaTipo, SP_FacturaTipo.Descrip AS DescFacturaTipo
    FROM (((SP_Afiliado INNER JOIN (SP_Factura INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) ON SP_Afiliado.CI = SP_Prestamo.CI) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_FacturaEstado ON SP_Factura.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado) INNER JOIN SP_FacturaTipo ON SP_Factura.CodFacturaTipo = SP_FacturaTipo.CodFacturaTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Pago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Pago]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.NroFactura, SP_Prestamo.IDPrestamo, SP_Pago.Importe, SP_Factura.Importe AS ImpFactura, (CASE WHEN [SP_Pago].[Importe]>SP_Factura.Importe THEN [SP_Pago].[Importe]-SP_Factura.Importe ELSE 0 END) AS ImpInteresMora, SP_Afiliado.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Factura.FechaVencimiento, SP_Pago.Fecha AS FechaPago, SP_Pago.CodSucursal, SP_Factura.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Factura.ImpAmortizable, SP_Factura.ImpInteres, SP_Pago.CodPagoOrigen, SP_PagoOrigen.Descrip AS DescPagoOrigen, SP_Pago.Usr, SP_Pago.Ts
    FROM ((((SP_Pago INNER JOIN SP_Factura ON SP_Pago.IDFactura = SP_Factura.IDFactura) INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Factura.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PagoOrigen ON SP_Pago.CodPagoOrigen = SP_PagoOrigen.CodPagoOrigen;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_PagoError =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_PagoError]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoError.IDFactura, SP_PagoError.Fecha, SP_PagoError.Importe, SP_PagoError.CodSucursal, SP_PagoError.TasaCambio, SP_PagoError.CodFacturaEstado, SP_FacturaEstado.Descrip AS DescFacturaEstado, SP_PagoError.Usr, SP_PagoError.Ts
    FROM SP_PagoError INNER JOIN SP_FacturaEstado ON SP_PagoError.CodFacturaEstado = SP_FacturaEstado.CodFacturaEstado;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_PrestamoCuadro =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_PrestamoCuadro]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Moneda.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_CuadroAmortizacion.NroCuota, SP_CuadroAmortizacion.Monto, SP_CuadroAmortizacion.ImporteCuota, SP_CuadroAmortizacion.Interes, SP_CuadroAmortizacion.Amortizacion, SP_CuadroAmortizacion.Saldo, SP_Prestamo.Promedio, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo
    FROM (((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_CuadroAmortizacion ON SP_Prestamo.IDPrestamo = SP_CuadroAmortizacion.IDPrestamo) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_RetencionItem_Cruz =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_RetencionItem_Cruz]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @cols NVARCHAR(MAX);
    DECLARE @sumCols NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(nvarchar(128), pv)), ',')
    FROM (SELECT DISTINCT [CodRetencionItemCod] AS pv FROM SP_RetencionItem) d;

    SELECT @sumCols = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(nvarchar(128), pv)) + ',0)', ' + ')
    FROM (SELECT DISTINCT [CodRetencionItemCod] AS pv FROM SP_RetencionItem) d;

    SET @sql = N'SELECT p.*' +
              CASE WHEN @sumCols IS NOT NULL THEN N', ' + @sumCols + N' AS [Total de Importe]' ELSE N'' END +
              N' FROM (SELECT [IDPrestamo], [Fecha], [CodRetencionItemCod] AS __PivotKey, [Importe] AS __PivotValue FROM SP_RetencionItem) src '
              + N' PIVOT (SUM(__PivotValue) FOR __PivotKey IN (' + ISNULL(@cols, N'') + N')) p';

    EXEC sp_executesql @sql;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_RetencionPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_RetencionPago]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPago.IDPrestamo, SP_RetencionPago.Fecha, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPago.Mes, SP_RetencionPago.Anio, SP_RetencionPago.Importe, SP_RetencionPago.Usr, SP_RetencionPago.Ts
    FROM (SP_RetencionPago INNER JOIN SP_RetencionPrestamo ON SP_RetencionPago.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_RetencionPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_RetencionPrestamo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPrestamo.IDPrestamo, SP_RetencionPrestamo.CI, SP_RetencionPrestamo.Fecha, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CodMoneda, SP_RetencionPrestamo.Importe, SP_RetencionPrestamo.Saldo, SP_RetencionPrestamo.ImpPago, SP_RetencionPrestamo.Usr, SP_RetencionPrestamo.Ts
    FROM SP_RetencionPrestamo INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoComentario =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_AfiliadoComentario]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_AfiliadoComentario.*
    FROM SP_AfiliadoComentario
    ORDER BY SP_AfiliadoComentario.CI, SP_AfiliadoComentario.Fecha DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Empresa_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_Empresa_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Empresa.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa
    FROM SP_Empresa
    WHERE (((SP_Empresa.Ficticia)= 0))
    ORDER BY SP_Empresa.Nombre;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_FacturaEstado_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_FacturaEstado_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_FacturaEstado.CodFacturaEstado, SP_FacturaEstado.Descrip
    FROM SP_FacturaEstado
    ORDER BY SP_FacturaEstado.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_FacturaTipo_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_FacturaTipo_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_FacturaTipo.CodFacturaTipo, SP_FacturaTipo.Descrip
    FROM SP_FacturaTipo
    ORDER BY SP_FacturaTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Moneda_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_Moneda_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Moneda.CodMoneda, SP_Moneda.Descrip, SP_Moneda.Tasa
    FROM SP_Moneda
    ORDER BY SP_Moneda.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PagoOrigen_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PagoOrigen_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PagoOrigen.CodPagoOrigen, SP_PagoOrigen.Descrip
    FROM SP_PagoOrigen
    ORDER BY SP_PagoOrigen.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoActivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PrestamoActivo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.*
    FROM SP_PrestamoEstado INNER JOIN SP_Prestamo ON SP_PrestamoEstado.CodPrestamoEstado = SP_Prestamo.CodPrestamoEstado
    WHERE (((SP_PrestamoEstado.Fin)= 0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoEstado_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PrestamoEstado_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PrestamoEstado.CodPrestamoEstado, SP_PrestamoEstado.Descrip
    FROM SP_PrestamoEstado
    ORDER BY SP_PrestamoEstado.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoInteresAmortizacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PrestamoInteresAmortizacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.IDPrestamo, Sum(SP_CuadroAmortizacion.Interes) AS Interes, Sum(SP_CuadroAmortizacion.Amortizacion) AS Amortizacion
    FROM SP_CuadroAmortizacion
    GROUP BY SP_CuadroAmortizacion.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoPctRetenciones =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PrestamoPctRetenciones]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT P.IDPrestamo, MIN(P.CI) AS CI, MIN(P.Fecha) AS Fecha, MIN(P.CodMoneda) AS CodMoneda, MIN(P.Importe) AS Importe, 0 AS Tasa, MIN(SP_PrestamoEstado.Descrip) AS DescPrestamoEstado, Count(*) AS Cant_Facturas, Sum((CASE WHEN F.CodFacturaEstado='ret' THEN 1 ELSE 0 END)) AS Cant_Fac_Ret, CASE WHEN Count(*)=0 THEN 0 ELSE Sum((CASE WHEN F.CodFacturaEstado='ret' THEN 1 ELSE 0 END))*1.0/Count(*) END AS Pct_Retenidas
    FROM (SP_Prestamo AS P INNER JOIN SP_Factura AS F ON P.IDPrestamo = F.IdPrestamo) INNER JOIN SP_PrestamoEstado ON P.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado
    WHERE (((SP_PrestamoEstado.Fin)= 1))
    GROUP BY P.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoTipo_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rs_PrestamoTipo_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_PrestamoTipo.CodPrestamoTipo, SP_PrestamoTipo.Descrip
    FROM SP_PrestamoTipo
    ORDER BY SP_PrestamoTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: wFactura =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_wFactura]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.*, SP_Prestamo_1.Fecha AS Fecha_Pre
    FROM ((SP_Factura INNER JOIN SP_Prestamo ON SP_Factura.IdPrestamo = SP_Prestamo.IDPrestamo) INNER JOIN SP_Prestamo AS SP_Prestamo_1 ON SP_Prestamo.IDPrestamoRef = SP_Prestamo_1.IDPrestamo) LEFT JOIN SP_Pago ON SP_Factura.IDFactura = SP_Pago.IDFactura
    WHERE (((SP_Factura.CodFacturaEstado)='car') AND ((SP_Pago.IDFactura) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: wRetencionDetalle =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_wRetencionDetalle]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IDPrestamo, Fecha, -SP_RetencionPago.Importe AS Importe
    FROM SP_RetencionPago
    UNION ALL SELECT IDPrestamo, Fecha, Importe 
    FROM SP_Retencion;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_FacturasCantidadXPrestamo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_FacturasCantidadXPrestamo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, Count(SP_Factura.IDFactura) AS CuentaDeIDFactura
    FROM SP_Factura
    GROUP BY SP_Factura.IdPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_FacturasDeCancelacionAnticipada =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_FacturasDeCancelacionAnticipada]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, Max(SP_Factura.IDFactura) AS IDFactura
    FROM tmp_Anticipados AS fa INNER JOIN SP_Factura ON fa.IDPrestamo = SP_Factura.IdPrestamo
    GROUP BY SP_Factura.IdPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_UpdateAnticipados =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_UpdateAnticipados]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaEstado = 'ana' FROM SP_Factura INNER JOIN tmp_Anticipados AS p ON SP_Factura.IdPrestamo = p.IDPrestamo
    WHERE (((SP_Factura.CodFacturaEstado)='anu'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_UpdateFacturasAnticipadas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_UpdateFacturasAnticipadas]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaTipo = 'can' FROM SP_Factura INNER JOIN tmp_FacturasAnticipadas AS fa ON SP_Factura.IDFactura = fa.IDFactura
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_UpdateFActuraTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_UpdateFActuraTipo]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SP_Factura SET SP_Factura.CodFacturaTipo = 'com';
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1000_AfiliadoPromedioMeses =====
-- DependsOn: 1000_AfiliadoImpLiquidoxMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1000_AfiliadoPromedioMeses]
    @pCI INT,
    @pAnioMesIni INT,
    @pAnioMesFin INT,
    @pMeses NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(acc_sp_1000_AfiliadoImpLiquidoxMes_q.Importe)/@pMeses AS Importe
    FROM [acc_sp_1000_AfiliadoImpLiquidoxMes_q]
    WHERE (((acc_sp_1000_AfiliadoImpLiquidoxMes_q.CI)=@pCI) AND ((acc_sp_1000_AfiliadoImpLiquidoxMes_q.AnioMes) Between @pAnioMesIni And @pAnioMesFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1009_PrestamoVale =====
-- DependsOn: 1009_MinFechaVencimiento
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1009_PrestamoVale]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Afiliado.Direccion, SP_Moneda.Descrip AS DescMoneda, SP_Moneda.DescripLarga AS DescMonedaLarga, SP_Prestamo.Cuotas, SP_Prestamo.ImporteCuota, acc_sp_1009_MinFechaVencimiento_q.FechaVencimiento, SP_Prestamo.Tasa, SP_Prestamo.Importe
    FROM ((SP_Prestamo INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN [acc_sp_1009_MinFechaVencimiento_q] ON SP_Prestamo.IDPrestamo = acc_sp_1009_MinFechaVencimiento_q.IDPrestamo
    WHERE (((SP_Prestamo.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Prestamo_Crystal =====
-- DependsOn: 1011_Prestamo_MinFechaVencimiento
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Prestamo_Crystal]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, acc_sp_1011_Prestamo_MinFechaVencimiento_q.FechaVencimiento, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo
    FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) LEFT JOIN [acc_sp_1011_Prestamo_MinFechaVencimiento_q] ON SP_Prestamo.IDPrestamo = acc_sp_1011_Prestamo_MinFechaVencimiento_q.IDPrestamo) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1015_Insert_Liquidos =====
-- DependsOn: 1015_CargaLiquidos
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1015_Insert_Liquidos]
    @pCodEmpresa NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SP_ImpLiquido ( CodEmpresa, Fechaingreso, CI, Importe, IdTrabaja, Mes, Anio, AnioMes, Usr, Ts )
    SELECT SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, acc_sp_1015_CargaLiquidos_q.CI, acc_sp_1015_CargaLiquidos_q.Importe, SP_Trabaja.IdTrabaja, @pMes AS Expr1, @pAnio AS Expr2, TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AS Expr5, @pUsr AS Expr3, SYSDATETIME() AS Expr4
    FROM SP_Trabaja INNER JOIN [acc_sp_1015_CargaLiquidos_q] AS acc_sp_1015_CargaLiquidos_q ON SP_Trabaja.CI = acc_sp_1015_CargaLiquidos_q.CI
    WHERE SP_Trabaja.CodEmpresa=@pCodEmpresa 
    AND (SP_Trabaja.FechaBaja Is Null Or (YEAR(SP_Trabaja.FechaBaja) * 100 + MONTH(SP_Trabaja.FechaBaja))>TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2))) AND (YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso]))<=TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1017_Insert_Liquidos_Casemed =====
-- DependsOn: 1017_LiquidoSubsidioxMesAnio
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1017_Insert_Liquidos_Casemed]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX),
    @pCodEmpresa NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SP_ImpLiquido ( CI, Mes, Anio, CodEmpresa, Fechaingreso, IdTrabaja, Importe, AnioMes, Usr, Ts )
    SELECT [1017_LiquidoSubsidioxMesAnio].CI, [1017_LiquidoSubsidioxMesAnio].Mes, [1017_LiquidoSubsidioxMesAnio].Anio, SP_Trabaja.CodEmpresa, SP_Trabaja.FechaIngreso, SP_Trabaja.IdTrabaja, [1017_LiquidoSubsidioxMesAnio].ImpLiquido, TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AS Expr5, @pUsr AS Expr1, SYSDATETIME() AS Expr2
    FROM [1017_LiquidoSubsidioxMesAnio](@pMes, @pAnio) AS [1017_LiquidoSubsidioxMesAnio] INNER JOIN SP_Trabaja ON [1017_LiquidoSubsidioxMesAnio].CI = SP_Trabaja.CI
    WHERE ((([1017_LiquidoSubsidioxMesAnio].Mes)=@pMes) AND (([1017_LiquidoSubsidioxMesAnio].Anio)=@pAnio) AND ((SP_Trabaja.CodEmpresa)=@pCodEmpresa) AND ((SP_Trabaja.FechaBaja) Is Null) AND (((YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])))<=TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)))) OR ((([1017_LiquidoSubsidioxMesAnio].Mes)=@pMes) AND (([1017_LiquidoSubsidioxMesAnio].Anio)=@pAnio) AND ((SP_Trabaja.CodEmpresa)=@pCodEmpresa) AND (((YEAR([SP_Trabaja].[FechaIngreso]) * 100 + MONTH([SP_Trabaja].[FechaIngreso])))<=TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2))) AND (((YEAR([SP_Trabaja].[FechaBaja]) * 100 + MONTH([SP_Trabaja].[FechaBaja])))>TRY_CONVERT(float,@pAnio + RIGHT('00' + CONVERT(varchar(2), @pMes), 2))));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1025_TotalPagoParcialPorPrestamo =====
-- DependsOn: 1025_PagoParcialFromPrestamo
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1025_TotalPagoParcialPorPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum([1025_PagoParcialFromPrestamo].Importe) AS Importe
    FROM [acc_sp_1025_PagoParcialFromPrestamo_q](@pIDPrestamo) AS [1025_PagoParcialFromPrestamo];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1030_FlujoTIR =====
-- DependsOn: 1030_PrestamoFlujo, acc_sp_1030_FacturaFlujo_q
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1030_FlujoTIR]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [acc_sp_1030_PrestamoFlujo_q]
    UNION ALL SELECT * FROM [acc_sp_1030_FacturaFlujo_q];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Retencion =====
-- DependsOn: 1120_Retencion_Amort, 1120_Retencion_Tel
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Retencion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.IDPrestamo, SP_RetencionPrestamo.CodEmpresa, SP_Empresa.Nombre AS DescEmpresa, SP_RetencionPrestamo.CI, SP_Retencion.Fecha, SP_Retencion.TipoCambio, SP_RetencionPrestamo.CodMoneda, acc_sp_1120_Retencion_Amort_q.Importe, acc_sp_1120_Retencion_Amort_q.ImpAmortizable, acc_sp_1120_Retencion_Amort_q.ImpInteres, acc_sp_1120_Retencion_Amort_q.ImpMora, acc_sp_1120_Retencion_Tel_q.Importe AS ImpTel, SP_Retencion.Observaciones, SP_Retencion.Usr, SP_Retencion.Ts
    FROM (((SP_Retencion INNER JOIN SP_RetencionPrestamo ON SP_Retencion.IDPrestamo = SP_RetencionPrestamo.IDPrestamo) INNER JOIN SP_Empresa ON SP_RetencionPrestamo.CodEmpresa = SP_Empresa.CodEmpresa) INNER JOIN [acc_sp_1120_Retencion_Amort_q] ON (SP_Retencion.Fecha = acc_sp_1120_Retencion_Amort_q.Fecha) AND (SP_Retencion.IDPrestamo = acc_sp_1120_Retencion_Amort_q.IDPrestamo)) LEFT JOIN [acc_sp_1120_Retencion_Tel_q] ON (SP_Retencion.Fecha = acc_sp_1120_Retencion_Tel_q.Fecha) AND (SP_Retencion.IDPrestamo = acc_sp_1120_Retencion_Tel_q.IDPrestamo);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_FacturaxIDPrestamo =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.Importe, Rpt_Factura.CodigoBarra, Rpt_Factura.Impresiones
    FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
    WHERE (((Rpt_Factura.IDPrestamo)=@pIDPrestamo) AND ((Rpt_Factura.CodFacturaEstado)<>'anu' And (Rpt_Factura.CodFacturaEstado)<>'can'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_FacturaxIDPrestamoEstado =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_FacturaxIDPrestamoEstado]
    @pIDPrestamo INT,
    @pCodFacturaEstado NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.Importe, Rpt_Factura.CodigoBarra, Rpt_Factura.CodFacturaEstado, Rpt_Factura.Impresiones
    FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
    WHERE (((Rpt_Factura.IDPrestamo)=@pIDPrestamo) AND ((Rpt_Factura.CodFacturaEstado)=@pCodFacturaEstado));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_FacturaxNroFactura =====
-- DependsOn: Rpt_Factura
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_FacturaxNroFactura]
    @pNroFactura INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Rpt_Factura.IDPrestamo, Rpt_Factura.CI, Rpt_Factura.Nombres, Rpt_Factura.Apellido1, Rpt_Factura.Apellido2, Rpt_Factura.Direccion, Rpt_Factura.NroFactura, Rpt_Factura.FechaEmitida, Rpt_Factura.FechaVencimiento, Rpt_Factura.Tasa, Rpt_Factura.CodMoneda, Rpt_Factura.DescMoneda, Rpt_Factura.Descrip, Rpt_Factura.Importe, Rpt_Factura.NroCuota, Rpt_Factura.Importe, Rpt_Factura.CodigoBarra, Rpt_Factura.Impresiones
    FROM [acc_sp_Rpt_Factura_q] AS Rpt_Factura
    WHERE (((Rpt_Factura.NroFactura)=@pNroFactura));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Tmp_Rpt_Factura =====
-- DependsOn: Rpt_Factura_DBG
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_500_Tmp_Rpt_Factura]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sp_Rpt_Factura_DBG_q] AS Rpt_Factura
    WHERE ( [Rpt_Factura].[FechaEmitida] <= TRY_CONVERT(datetime2,'31/12/2007') and ([Rpt_Factura].[FechaPago] > TRY_CONVERT(datetime2,'31/12/2007') or [Rpt_Factura].[FechaPago] Is Null) and [Rpt_Factura].[CodFacturaEstado] <> 'anu' and [Rpt_Factura].[CodFacturaEstado] <> 'cle' );
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 2000_Rpt_PrestamoCuadroxIDPrestamo =====
-- DependsOn: Rpt_PrestamoCuadro
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_2000_Rpt_PrestamoCuadroxIDPrestamo]
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Rpt_PrestamoCuadro.*
    FROM [acc_sp_Rpt_PrestamoCuadro_q] AS Rpt_PrestamoCuadro
    WHERE (((Rpt_PrestamoCuadro.IDPrestamo)=@pIDPrestamo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1116_AfiliadoComentarioXCI =====
-- DependsOn: Rs_AfiliadoComentario
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1116_AfiliadoComentarioXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Rs_AfiliadoComentario.*
    FROM [acc_sp_Rs_AfiliadoComentario_q] AS Rs_AfiliadoComentario
    WHERE (((Rs_AfiliadoComentario.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1022_ImporteCuotaPrestamoActivoXCI =====
-- DependsOn: Rs_PrestamoActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1022_ImporteCuotaPrestamoActivoXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 SP_Cuota.Importe, acc_sp_Rs_PrestamoActivo_q.CodMoneda
    FROM ([acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q INNER JOIN SP_Cuota ON acc_sp_Rs_PrestamoActivo_q.IDPrestamo = SP_Cuota.IDPrestamo)
    WHERE (((acc_sp_Rs_PrestamoActivo_q.CI)=@pCI))
    ORDER BY SP_Cuota.Nro;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1022_PrestamoActivoXCI =====
-- DependsOn: Rs_PrestamoActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1022_PrestamoActivoXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sp_Rs_PrestamoActivo_q.*
    FROM [acc_sp_Rs_PrestamoActivo_q] AS acc_sp_Rs_PrestamoActivo_q
    WHERE (((acc_sp_Rs_PrestamoActivo_q.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Prestamo =====
-- DependsOn: Rs_PrestamoInteresAmortizacion
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Prestamo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.IDPrestamoRef, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo, acc_sp_Rs_PrestamoInteresAmortizacion_q.Amortizacion, acc_sp_Rs_PrestamoInteresAmortizacion_q.Interes
    FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo) LEFT JOIN [acc_sp_Rs_PrestamoInteresAmortizacion_q] ON SP_Prestamo.IDPrestamo = acc_sp_Rs_PrestamoInteresAmortizacion_q.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Prestamo2 =====
-- DependsOn: Rs_PrestamoInteresAmortizacion
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Rpt_Prestamo2]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo, SP_Prestamo.CI, SP_Afiliado.Nombres, SP_Afiliado.Apellido1, SP_Afiliado.Apellido2, SP_Prestamo.Fecha, SP_Prestamo.FechaCobro, SP_Prestamo.CodMoneda, SP_Moneda.Descrip AS DescMoneda, SP_Prestamo.Importe, SP_Prestamo.Cuotas, SP_Prestamo.Tasa, SP_Prestamo.Saldo, SP_Prestamo.CodPrestamoEstado, SP_PrestamoEstado.Descrip AS DescPrestamoEstado, SP_Prestamo.IDPrestamoRef, SP_Prestamo.Usr, SP_Prestamo.Ts, SP_Prestamo.CodPrestamoTipo, SP_PrestamoTipo.Descrip AS DescPrestamoTipo, acc_sp_Rs_PrestamoInteresAmortizacion_q.Interes, acc_sp_Rs_PrestamoInteresAmortizacion_q.Amortizacion
    FROM ((((SP_Prestamo INNER JOIN SP_Afiliado ON SP_Prestamo.CI = SP_Afiliado.CI) INNER JOIN SP_Moneda ON SP_Prestamo.CodMoneda = SP_Moneda.CodMoneda) INNER JOIN SP_PrestamoEstado ON SP_Prestamo.CodPrestamoEstado = SP_PrestamoEstado.CodPrestamoEstado) INNER JOIN SP_PrestamoTipo ON SP_Prestamo.CodPrestamoTipo = SP_PrestamoTipo.CodPrestamoTipo) INNER JOIN [acc_sp_Rs_PrestamoInteresAmortizacion_q] ON SP_Prestamo.IDPrestamo = acc_sp_Rs_PrestamoInteresAmortizacion_q.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1115_PrestamosAnterioresxCI =====
-- DependsOn: Rs_PrestamoPctRetenciones
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1115_PrestamosAnterioresxCI]
    @pCI INT,
    @pIDPrestamo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sp_Rs_PrestamoPctRetenciones_q.*
    FROM [acc_sp_Rs_PrestamoPctRetenciones_q]
    WHERE (((acc_sp_Rs_PrestamoPctRetenciones_q.IDPrestamo)<(CASE WHEN @pIDPrestamo>0 THEN @pIDPrestamo ELSE 999999999 END)) AND ((acc_sp_Rs_PrestamoPctRetenciones_q.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: wSaldoSum =====
-- DependsOn: wRetencionDetalle
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_wSaldoSum]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT wRetencionDetalle.IDPrestamo, TRY_CONVERT(int, Sum(wRetencionDetalle.Importe)) AS SumaDeImporte
    FROM [acc_sp_wRetencionDetalle_q] AS wRetencionDetalle INNER JOIN SP_RetencionPrestamo ON wRetencionDetalle.IDPrestamo = SP_RetencionPrestamo.IDPrestamo
    WHERE (((wRetencionDetalle.Fecha)<CONVERT(date, '2006-01-01')))
    GROUP BY wRetencionDetalle.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_PrestamosAnticipados =====
-- DependsOn: xw_FacturasCantidadXPrestamo
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_xw_PrestamosAnticipados]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Prestamo.IDPrestamo
    FROM SP_Prestamo INNER JOIN [acc_sp_xw_FacturasCantidadXPrestamo_q] ON SP_Prestamo.IDPrestamo = acc_sp_xw_FacturasCantidadXPrestamo_q.IdPrestamo
    WHERE (((acc_sp_xw_FacturasCantidadXPrestamo_q.CuentaDeIDFactura)>[Cuotas]));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1030_Insert_rptTIR =====
-- DependsOn: 1030_FlujoTIR
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1030_Insert_rptTIR]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO rptTIR_Tmp ( IDPrestamo, Mes, Importe )
    SELECT acc_sp_1030_FlujoTIR_q.IDPrestamo, acc_sp_1030_FlujoTIR_q.Mes, Sum(acc_sp_1030_FlujoTIR_q.Importe) AS SumaDeImporte
    FROM [acc_sp_1030_FlujoTIR_q]
    GROUP BY acc_sp_1030_FlujoTIR_q.IDPrestamo, acc_sp_1030_FlujoTIR_q.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Buscar duplicados por 1030_FlujoTIR =====
-- DependsOn: 1030_FlujoTIR
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_Buscar_duplicados_por_1030_FlujoTIR]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT [IDPrestamo], [Mes], [Importe]
    FROM [acc_sp_1030_FlujoTIR_q]
    WHERE [IDPrestamo] In (SELECT [IDPrestamo] FROM [acc_sp_1030_FlujoTIR_q] As Tmp GROUP BY [IDPrestamo],[Mes] HAVING Count(*)>1  And [Mes] = acc_sp_1030_FlujoTIR_q.[Mes])
    ORDER BY [IDPrestamo], [Mes];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1130_FacturaInteresMes =====
-- DependsOn: 500_Tmp_Rpt_Factura
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1130_FacturaInteresMes]
    @pCodMoneda NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdPrestamo, TRY_CONVERT(datetime2,'01/' + FORMAT(FechaVencimiento,'mm/yy')) AS Mes, ImpInteres
    FROM [acc_sp_500_Tmp_Rpt_Factura_q]
    WHERE (((CodFacturaEstado)<>'anu') AND ((CodMoneda)=@pCodMoneda));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1130_CuadroFacturaInteres =====
-- DependsOn: 1130_FacturaInteresMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1130_CuadroFacturaInteres]
    @pCodMoneda NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @cols NVARCHAR(MAX);
    DECLARE @sumCols NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(nvarchar(128), pv)), ',')
    FROM (SELECT DISTINCT [1130_FacturaInteresMes].IdPrestamo AS pv FROM [1130_FacturaInteresMes](@pCodMoneda)) d;

    SELECT @sumCols = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(nvarchar(128), pv)) + ',0)', ' + ')
    FROM (SELECT DISTINCT [1130_FacturaInteresMes].IdPrestamo AS pv FROM [1130_FacturaInteresMes](@pCodMoneda)) d;

    SET @sql = N'SELECT p.*' +
              CASE WHEN @sumCols IS NOT NULL THEN N', ' + @sumCols + N' AS [Total de ImpInteres]' ELSE N'' END +
              N' FROM (SELECT [1130_FacturaInteresMes].Mes, [1130_FacturaInteresMes].IdPrestamo AS __PivotKey, [1130_FacturaInteresMes].ImpInteres AS __PivotValue FROM [1130_FacturaInteresMes](@pCodMoneda)) src '
              + N' PIVOT (SUM(__PivotValue) FOR __PivotKey IN (' + ISNULL(@cols, N'') + N')) p';

    EXEC sp_executesql @sql, N'@pCodMoneda NVARCHAR(MAX)', @pCodMoneda=@pCodMoneda;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 1130_CuadroFacturaInteres2 =====
-- DependsOn: 1130_FacturaInteresMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sp_1130_CuadroFacturaInteres2]
    @pCodMoneda NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @cols NVARCHAR(MAX);
    DECLARE @sumCols NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(nvarchar(128), pv)), ',')
    FROM (SELECT DISTINCT [1130_FacturaInteresMes].Mes AS pv FROM [1130_FacturaInteresMes](@pCodMoneda)) d;

    SELECT @sumCols = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(nvarchar(128), pv)) + ',0)', ' + ')
    FROM (SELECT DISTINCT [1130_FacturaInteresMes].Mes AS pv FROM [1130_FacturaInteresMes](@pCodMoneda)) d;

    SET @sql = N'SELECT p.*' +
              CASE WHEN @sumCols IS NOT NULL THEN N', ' + @sumCols + N' AS [Total de ImpInteres]' ELSE N'' END +
              N' FROM (SELECT [1130_FacturaInteresMes].IdPrestamo, [1130_FacturaInteresMes].Mes AS __PivotKey, [1130_FacturaInteresMes].ImpInteres AS __PivotValue FROM [1130_FacturaInteresMes](@pCodMoneda)) src '
              + N' PIVOT (SUM(__PivotValue) FOR __PivotKey IN (' + ISNULL(@cols, N'') + N')) p';

    EXEC sp_executesql @sql, N'@pCodMoneda NVARCHAR(MAX)', @pCodMoneda=@pCodMoneda;
END;

GO

