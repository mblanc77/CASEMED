-- Auto-generated from sgpa.mdb-specs.txt
-- Query count: 427
-- Ordered by dependencies (nested queries first)

-- ===== DATA OBJECT FOR QUERY: 001_AfeccionGrupo_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_AfeccionGrupo_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodAfeccionGrupo) + '') + 1 AS Max
FROM AfeccionGrupo;
GO

-- ===== DATA OBJECT FOR QUERY: 001_AfeccionTipo_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_AfeccionTipo_Max_q]
AS
SELECT (CASE WHEN TRY_CONVERT(float,Max(CodAfeccionTipo) + '') + 1 = 9999 THEN 10000 ELSE TRY_CONVERT(float,Max(CodAfeccionTipo) + '') + 1 END) AS Max
FROM AfeccionTipo
WHERE CodAfeccionTipo <> 9999;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Certificacion_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Certificacion_Max_q]
AS
SELECT TRY_CONVERT(float,Max(NroLlamado) + '') + 1 AS Max
FROM Certificacion;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Certificador_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Certificador_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodCertificador) + '') + 1 AS Max
FROM Certificador;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Empresa_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Empresa_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodEmpresa) + '')+1 AS Max
FROM Empresa
WHERE (((Empresa.CodEmpresa)<>900));
GO

-- ===== DATA OBJECT FOR QUERY: 001_FormaPago_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_FormaPago_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodFormaPago) + '') + 1 AS Max
FROM FormaPago;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Mutualista_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Mutualista_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodMutualista) + '') + 1 AS Max
FROM Mutualista;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Patologia_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Patologia_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodPatologia) + '') + 1 AS Max
FROM Patologia;
GO

-- ===== DATA OBJECT FOR QUERY: 001_PrestacionTipo_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_PrestacionTipo_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodPrestacionTipo) + '') + 1 AS Max
FROM PrestacionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 001_Recibo_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_Recibo_Max_q]
AS
SELECT TRY_CONVERT(float,Max(NroRecibo) + '')+1 AS Max
FROM SubsidioCabezal;
GO

-- ===== DATA OBJECT FOR QUERY: 001_RegimenJubilatorio_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_RegimenJubilatorio_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodRegimenJubilatorio) + '') + 1 AS Max
FROM RegimenJubilatorio;
GO

-- ===== DATA OBJECT FOR QUERY: 001_SalidaTipo_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_SalidaTipo_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodSalidaTipo) + '')+1 AS Max
FROM SalidaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 001_SituacionPago_Max =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_001_SituacionPago_Max_q]
AS
SELECT TRY_CONVERT(float,Max(CodSituacionPago) + '') + 1 AS Max
FROM SituacionPago;
GO

-- ===== DATA OBJECT FOR QUERY: 100_Afiliado_CI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_100_Afiliado_CI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.CI, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
FROM Afiliado
WHERE (((Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 100_CargadoHL =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_100_CargadoHL_q](@pCodEmpresa NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pAnio INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio
FROM Imponible
WHERE (((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 100_CargadosHL =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_100_CargadosHL_q](@pMes TINYINT, @pAnio INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI
FROM Imponible
WHERE (((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio) AND ((Imponible.CodEmpresa)=@pCodEmpresa))
GROUP BY Imponible.CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 100_TrabajaActivo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_100_TrabajaActivo_q](@pMes INT, @pAno INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.*
FROM Trabaja
WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>=TRY_CONVERT(datetime2,'01/' + CONVERT(nvarchar(50), @pMes) + '/' + CONVERT(nvarchar(50), @pAno) )))
)
GO

-- ===== DATA OBJECT FOR QUERY: 101_ImponibleMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_101_ImponibleMes_q](@pMes INT, @pAno INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.*
FROM Imponible
WHERE (((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAno))
)
GO

-- ===== DATA OBJECT FOR QUERY: 102_DiasCertificados =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_102_DiasCertificados_q](@pCI INT, @pNroLlamado INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum(DATEDIFF(day,FechaIni,FechaFin)+1) AS Dias, Count(*) AS Cantidad
FROM Certificacion
WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.Efectiva)= 1) AND ((Certificacion.NroLlamado) <= (CASE WHEN @pNroLlamado = 0 THEN Certificacion.NroLlamado ELSE @pNroLlamado END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 102_Prestacion_Cantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_102_Prestacion_Cantidad_q](@pCI INT, @pCodPrestacionTipo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Prestacion
WHERE (((Prestacion.CI)=@pCI) AND ((Prestacion.CodPrestacionTipo)=@pCodPrestacionTipo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 103_PrestacionesAfiliado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_103_PrestacionesAfiliado_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Prestacion.CI, Prestacion.Fecha, PrestacionTipo.Descrip AS Tipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta
FROM Prestacion INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
WHERE (((Prestacion.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 103_ReintegrosAfiliado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_103_ReintegrosAfiliado_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT ReintegroMutual.CI, ReintegroMutual.Anio + '/' + RIGHT('00' + CONVERT(varchar(2), ReintegroMutual.Mes), 2) AS AnioMes, ReintegroMutual.Fecha, Mutualista.Descrip AS Mutualista, ReintegroMutual.Importe
FROM ReintegroMutual INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista
WHERE (((ReintegroMutual.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 110_Imponible_Periodo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_110_Imponible_Periodo_q](@pCI INT, @pMesIni INT, @pMesFin INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum(Imponible.Importe) AS Importe
FROM Imponible
WHERE (((Imponible.CI)=@pCI) AND ((Imponible.AnioMes) Between @pMesIni And @pMesFin) AND ((Imponible.Concepto)='1'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 110_PrimaFallecimiento_CI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_110_PrimaFallecimiento_CI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT PrimaFallecimiento.*
FROM PrimaFallecimiento
WHERE (((PrimaFallecimiento.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 145_AfiliadoXCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_145_AfiliadoXCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.*
FROM Afiliado
WHERE (((Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 150_5_Mejores_Pagos_No_Casmu =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_150_5_Mejores_Pagos_No_Casmu_q]
AS
SELECT TOP 5 Afiliado.CI, I.Mes, I.Anio, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres, Empresa.Nombre, I.Importe
FROM (Imponible AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) INNER JOIN Empresa ON I.CodEmpresa = Empresa.CodEmpresa
WHERE (((I.CodEmpresa)<>1));
GO

-- ===== DATA OBJECT FOR QUERY: 160_Trabaja =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_160_Trabaja_q](@pCI INT, @pCodEmpresa INT, @pFechaIngreso DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.*
FROM Trabaja
WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.CodEmpresa)=@pCodEmpresa) AND ((Trabaja.FechaIngreso)=@pFechaIngreso))
)
GO

-- ===== DATA OBJECT FOR QUERY: 160_Trabaja_CI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_160_Trabaja_CI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.*
FROM Trabaja
WHERE (((Trabaja.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 180_GrupoEtario_EdadIni =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_180_GrupoEtario_EdadIni_q](@pEdadIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT GrupoEtario.*
FROM GrupoEtario
WHERE (((GrupoEtario.EdadIni)=@pEdadIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 200_Imponible =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_200_Imponible_q](@pMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.*, Trabaja.FechaBaja
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Trabaja.FechaBaja) Is Null)) OR ((((YEAR(DATEADD(month, -1, [Trabaja].[FechaBaja])) * 100 + MONTH(DATEADD(month, -1, [Trabaja].[FechaBaja]))))>@pMes))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_ImpRetObrero =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_ImpRetObrero_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
WHERE (((SubsidioItem.CodSubsidioItemCod) In (1,2,3,4,5,6,7)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_ImpRetPatronal =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_ImpRetPatronal_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
WHERE (((SubsidioItem.CodSubsidioItemCod) In (101, 102)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_ImpRetTotal =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_ImpRetTotal_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
WHERE (((SubsidioItem.CodSubsidioItemCod) In (1,2,3,4,5,6,7,101,102)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_MontoGrabado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_MontoGrabado_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum([SubsidioCabezal].[ImpAguinaldo])+Sum([SubsidioCabezal].[ImpNominal]) AS Importe
FROM SubsidioCabezal
WHERE (((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_SubsidioCantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_SubsidioCantidad_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Count(SubsidioCabezal.CI) AS Cantidad
FROM SubsidioCabezal
WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_TotImpEmp =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_TotImpEmp_q](@pMes NVARCHAR(MAX), @pAnio INT)
RETURNS TABLE
AS
RETURN
(
SELECT EmpresaPago.Mes, EmpresaPago.Anio, Sum(EmpresaPago.Importe) AS Importe, ((Sum(EmpresaPago.Importe) * 0.5) / 100) AS TributoTotImpMut
FROM EmpresaPago
GROUP BY EmpresaPago.Mes, EmpresaPago.Anio
HAVING (((EmpresaPago.Mes)=@pMes) AND ((EmpresaPago.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 210_TotTributo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_210_TotTributo_q](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
WHERE (((SubsidioItem.CodSubsidioItemCod)=0 Or (SubsidioItem.CodSubsidioItemCod) = 100) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 220_AfiliadoImponibleMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_220_AfiliadoImponibleMes_q](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
WHERE (((Imponible.CI)=@pCI) AND (AnioMes Between @pMesIni And @pMes) AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1'))
GROUP BY Imponible.Anio, Imponible.Mes
)
GO

-- ===== DATA OBJECT FOR QUERY: 220_AfiliadoImponibleMes_Old =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_220_AfiliadoImponibleMes_Old_q](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Imponible.CI)=@pCI) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMes) AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1'))
GROUP BY Imponible.Anio, Imponible.Mes
)
GO

-- ===== DATA OBJECT FOR QUERY: 230_PrestacionAnterior =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_230_PrestacionAnterior_q](@pCI INT, @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
SELECT Prestacion.CI, Max(Prestacion.Fecha) AS Fecha, MIN(0) AS PeriodoRenovacion
FROM Prestacion INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
WHERE (((Prestacion.CI)=@pCI) AND ((Prestacion.CodPrestacionTipo)=@pCodPrestacionTipo))
GROUP BY Prestacion.CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 230_PrestacionTipoFromCod =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_230_PrestacionTipoFromCod_q](@pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
SELECT PrestacionTipo.*
FROM PrestacionTipo
WHERE (((PrestacionTipo.CodPrestacionTipo)=@pCodPrestacionTipo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 230_Receta =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_230_Receta_q](@pCI INT, @pFecha DATETIME2(0), @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
SELECT *
FROM Receta AS R
WHERE ((([R].[CI])=@pCI) AND (([R].[Fecha])=@pFecha) AND (([R].[CodPrestacionTipo])=@pCodPrestacionTipo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 230_Receta_Anterior =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_230_Receta_Anterior_q](@pCI INT, @pFecha DATETIME2(0), @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
SELECT *
FROM Receta AS R
WHERE R.CI=@pCI AND R.Fecha<@pFecha AND R.CodPrestacionTipo=@pCodPrestacionTipo
AND EXISTS
(SELECT 1 FROM Receta AS R2
WHERE R2.CI=@pCI AND R2.Fecha<@pFecha AND R2.CodPrestacionTipo=@pCodPrestacionTipo
HAVING MAX(R2.Fecha) = R.Fecha)
)
GO

-- ===== DATA OBJECT FOR QUERY: 230_Receta_Ultima =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_230_Receta_Ultima_q](@pCI INT, @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
SELECT *
FROM Receta AS RP
WHERE RP.CI=@pCI AND RP.CodPrestacionTipo=@pCodPrestacionTipo
AND EXISTS
(SELECT 1 FROM Receta AS RP2
WHERE RP2.CI=@pCI AND RP2.CodPrestacionTipo=@pCodPrestacionTipo
HAVING MAX(RP2.Fecha) = RP.Fecha)
)
GO

-- ===== DATA OBJECT FOR QUERY: 240_Grupos_IE =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_240_Grupos_IE_q]
AS
SELECT DISTINCT InformeEstadistico.Grupo
FROM InformeEstadistico;
GO

-- ===== DATA OBJECT FOR QUERY: 240_InformeGrupo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_240_InformeGrupo_q](@pGrupo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT InformeEstadistico.*
FROM InformeEstadistico
WHERE (((InformeEstadistico.Grupo)=@pGrupo))
)
GO

-- ===== DATA OBJECT FOR QUERY: 240_InformeIdRpt =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_240_InformeIdRpt_q](@pIdRpt INT)
RETURNS TABLE
AS
RETURN
(
SELECT InformeEstadistico.*
FROM InformeEstadistico
WHERE (((InformeEstadistico.IdRpt)=@pIdRpt))
)
GO

-- ===== DATA OBJECT FOR QUERY: 250_ActivosXEmpresaAUnaFecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Empresa.CodEmpresa, Empresa.Nombre, Count(Trabaja.IdTrabaja) AS Cantidad
FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>@pFecha) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngreso)<=@pFecha))
GROUP BY Empresa.CodEmpresa, Empresa.Nombre
)
GO

-- ===== DATA OBJECT FOR QUERY: 250_AportantesAUnMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_250_AportantesAUnMes_q](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CodEmpresa, Empresa.Nombre, Count(Trabaja.IdTrabaja) AS Cantidad
FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.IdTrabaja) In (select IdTrabaja From Imponible Where AnioMes = @pAnioMes AND Importe > 0)))
GROUP BY Trabaja.CodEmpresa, Empresa.Nombre
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_AfiliadoAporteOk =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp INT, @pMesFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.Fechaingreso, Imponible.CodEmpresa, Count(Imponible.CI) AS Cantidad
FROM Imponible
WHERE (((Imponible.Concepto)='1') AND ((Imponible.Importe)>0) AND ((Imponible.AnioMes)>=@pMesIniImp And (Imponible.AnioMes)<=@pMesFin))
GROUP BY Imponible.CI, Imponible.Fechaingreso, Imponible.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_CertificacionCIDia =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_CertificacionCIDia_q](@pCI INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Certificacion.NroLlamado, Certificacion.CI, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin FROM Certificacion
WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.FechaFin)=@pFecha) AND ((Certificacion.Efectiva)= 1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_CertificacionesAfiliadoMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_CertificacionesAfiliadoMes_q](@pCI INT, @pMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.ImporteDeducible, Certificacion.CodSalidaTipo
FROM Certificacion
WHERE (((Certificacion.CI)=@pCI) AND ((@pMes) Between (YEAR([FechaIni]) * 100 + MONTH([FechaIni])) And (YEAR([FechaFin]) * 100 + MONTH([FechaFin]))) AND ((Certificacion.Efectiva)= 1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_CertificacionesTmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_300_CertificacionesTmp_q]
AS
SELECT CertificacionesTmp.FechaIni, CertificacionesTmp.FechaFin, CertificacionesTmp.ImporteDeducible, CertificacionesTmp.CodSalidaTipo, CertificacionesTmp.CI
FROM CertificacionesTmp;
GO

-- ===== DATA OBJECT FOR QUERY: 300_EmpresaxIDSubsidio =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_EmpresaxIDSubsidio_q](@pIDSubsidio INT)
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioImponible.CodEmpresa, MIN(Empresa.Nombre) AS DescEmpresa
FROM SubsidioImponible INNER JOIN Empresa ON SubsidioImponible.CodEmpresa = Empresa.CodEmpresa
WHERE (((SubsidioImponible.IdSubsidio)=@pIDSubsidio))
GROUP BY SubsidioImponible.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_JornalAnterior =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_JornalAnterior_q](@pMes INT, @pCi INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.ValorJornal, SubsidioEnfermedad.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio
FROM (Certificacion AS C INNER JOIN SubsidioEnfermedad ON C.NroLlamado = SubsidioEnfermedad.IdSubsidio) INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
WHERE (((C.Efectiva)= 1) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C].[FechaIni])) + FORMAT(MONTH([C].[FechaIni]),'00')))<@pMes) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C].[FechaFin])) + FORMAT(MONTH([C].[FechaFin]),'00')))>=@pMes) AND ((C.CI)=@pCi) AND ((SubsidioCabezal.Liquidar)=@pLiquidar)) OR (((C.Efectiva)= 1) AND ((C.CI)=@pCi) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND (EXISTS(SELECT * FROM Certificacion C1 Where C1.Efectiva = 1 And C1.CI = @pCi  And TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C1].[FechaIni])) + FORMAT(MONTH([C1].[FechaIni]),'00')) = @pMes And DATEDIFF(day, C.FechaFin, C1.FechaIni) = 1
)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_JornalAnterior2 =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_JornalAnterior2_q](@pCI INT, @pLiquidar NVARCHAR(MAX), @pMes INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.ValorJornal
FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
WHERE ((((YEAR([SubsidioEnfermedad].[FechaIni]) * 100 + MONTH([SubsidioEnfermedad].[FechaIni])))<@pMes) AND (((YEAR([SubsidioEnfermedad].[FechaFin]) * 100 + MONTH([SubsidioEnfermedad].[FechaFin])))>=@pMes) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI)) OR (((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI) AND ((DATEDIFF(day,[SubsidioEnfermedad].[FechaFin],@pFecha))=1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_JornalAnterior2Ant =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_JornalAnterior2Ant_q](@pCI INT, @pLiquidar NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.ValorJornal
FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
WHERE (((SubsidioEnfermedad.FechaIni)=@pFechaIni) AND ((SubsidioEnfermedad.FechaFin)=@pFechaFin) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_RegimenJubilatorioAfiliado =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_RegimenJubilatorioAfiliado_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.CodRegimenJubilatorio
FROM Afiliado
WHERE (((Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_Rpt_PrimaFallecimiento =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_300_Rpt_PrimaFallecimiento_q]
AS
SELECT Afiliado.CI, YEAR([FechaFallecimiento]) AS Anio, MONTH([FechaFallecimiento]) AS Mes, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, PrimaFallecimiento.Importe AS ImpNominal, 0 AS Importe, PrimaFallecimiento.FechaFallecimiento, TRY_CONVERT(datetime2,'01/' + FORMAT(DATEADD(month,-6,PrimaFallecimiento.FechaFallecimiento),'mm/yyyy')) AS FechaIni, DATEADD(day,-1,TRY_CONVERT(datetime2,'01/' + FORMAT(PrimaFallecimiento.FechaFallecimiento,'mm/yyyy'))) AS FechaFin
FROM Afiliado INNER JOIN PrimaFallecimiento ON Afiliado.CI = PrimaFallecimiento.CI
WHERE (((PrimaFallecimiento.FechaFallecimiento) Is NOT Null));
GO

-- ===== DATA OBJECT FOR QUERY: 300_Rpt_Subsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_300_Rpt_Subsidio_q]
AS
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioItem.CodSubsidioItemCod, SubsidioItem.Importe, SubsidioItemCod.Descrip AS DescSubsidioItemCod, SubsidioItemCod.Tipo, SubsidioItemCod.Signo, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio, SubsidioCabezal.NroRecibo, [600_SubsidioFecha_Tmp].DescFecha, Afiliado.CodBanco, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta
FROM (((SubsidioCabezal INNER JOIN (SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod) ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio) INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco
WHERE (((SubsidioItemCod.Tipo)='O') AND ((TRY_CONVERT(float,[SubsidioCabezal].[ImpLiquido]))>0));
GO

-- ===== DATA OBJECT FOR QUERY: 300_Rpt_SubsidioEmpresa =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_300_Rpt_SubsidioEmpresa_q]
AS
SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezalEmpresa.ImpNominal, SubsidioCabezalEmpresa.ImpAguinaldo, SubsidioCabezalEmpresa.ImpLiquido, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio, SubsidioCabezal.NroRecibo, [600_SubsidioFecha_Tmp].DescFecha, Empresa.Nombre AS DescEmpresa, Afiliado.CodBanco, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta FROM ((((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) INNER JOIN SubsidioCabezalEmpresa ON SubsidioCabezal.IdSubsidio = SubsidioCabezalEmpresa.IdSubsidio) INNER JOIN Empresa ON SubsidioCabezalEmpresa.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco
WHERE (((TRY_CONVERT(float,[SubsidioCabezal].[ImpLiquido]))>0));
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioEmpresaAnterior =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioEmpresaAnterior_q](@pIdSubsidio INT)
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezalEmpresa.*
FROM SubsidioCabezalEmpresa
WHERE (((SubsidioCabezalEmpresa.IdSubsidio)=@pIdSubsidio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioEmpresaAnteriorVacia =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVacia_q](@pIdSubsidio INT, @pLiquidar NVARCHAR(MAX), @pCodCasemed NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioImponible.CodEmpresa, Sum(SubsidioImponible.Dias) AS Dias, Sum(SubsidioImponible.Importe) AS Importe, (CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END) AS Promedio
FROM SubsidioImponible INNER JOIN SubsidioCabezal ON SubsidioImponible.IdSubsidio = SubsidioCabezal.IdSubsidio
WHERE (((SubsidioImponible.IdSubsidio)=@pIdSubsidio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioImponible.CodEmpresa)<>@pCodCasemed))
GROUP BY SubsidioImponible.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioEmpresaAnteriorVaciaCasemed =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVaciaCasemed_q](@pIdSubsidio INT, @pLiquidar NVARCHAR(MAX), @pCodCasemed NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioImponible.CodEmpresa, Sum(SubsidioImponible.Dias) AS Dias, Sum(SubsidioImponible.Importe) AS Importe, Sum(SubsidioImponible.Importe)/180 AS Promedio
FROM SubsidioImponible INNER JOIN SubsidioCabezal ON SubsidioImponible.IdSubsidio = SubsidioCabezal.IdSubsidio
WHERE (((SubsidioImponible.IdSubsidio)=@pIdSubsidio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioImponible.CodEmpresa)=@pCodCasemed))
GROUP BY SubsidioImponible.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioEnfermedadFromMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioEnfermedadFromMes_q](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
FROM SubsidioEnfermedad INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
WHERE (((SubsidioCabezal.ImpNominal)>0) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioFecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioFecha_q](@pMes INT, @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
FROM SubsidioEnfermedad INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
WHERE (((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioFranjaAnt =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioFranjaAnt_q](@pCodSubsidioItemCod INT, @pImporte NVARCHAR(MAX), @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT (IRPControl.SMNAnt*@pSMN)-(((IRPControl.SMNAnt*@pSMN)*IRPControl.FranjaAnt)/100) AS ImpFrjAnt
FROM IRPControl
WHERE ((((IRPControl.SMNAnt*@pSMN)-(((IRPControl.SMNAnt*@pSMN)*IRPControl.FranjaAnt)/100))>@pImporte) AND ((IRPControl.CodIRP)=@pCodSubsidioItemCod))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioImporte =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioImporte_q](@pIdSubsidio INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum(SubsidioItem.Importe * SubsidioItemCod.Signo) AS Importe
FROM SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod
WHERE (((SubsidioItem.IdSubsidio)=@pIdSubsidio) AND ((SubsidioItemCod.Tipo)='O'))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioItemCod =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioItemCod_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioItemCod.*
FROM SubsidioItemCod
WHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioItemId =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioItemId_q](@pCodSubsidioItemCod INT)
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioItemCod.*
FROM SubsidioItemCod
WHERE (((SubsidioItemCod.CodSubsidioItemCod)=@pCodSubsidioItemCod))
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_TrabajaActivo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_TrabajaActivo_q](@pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.*
FROM Trabaja
WHERE (Trabaja.FechaBaja Is Null OR (YEAR([Trabaja].[FechaBaja]) * 100 + MONTH([Trabaja].[FechaBaja]))>@pMes) And (YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed]))<=@pMes
)
GO

-- ===== DATA OBJECT FOR QUERY: 310_CertificacionAnterior =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_310_CertificacionAnterior_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCI INT, @pNroLlamado INT)
RETURNS TABLE
AS
RETURN
(
SELECT Certificacion.NroLlamado, Certificacion.NroRecibo, Certificacion.FechaIni, Certificacion.FechaFin
FROM Certificacion
WHERE (((Certificacion.NroLlamado)<>@pNroLlamado) AND ((Certificacion.Efectiva)= 1) AND ((Certificacion.CI)=@pCI) AND (([Certificacion].[FechaIni]<=@pFechaFin And [Certificacion].[FechaFin]>=@pFechaIni)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 400_Suma_Importe =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Suma_Importe_q]
AS
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 400_Suma_Puestos =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Suma_Puestos_q]
AS
SELECT Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible
GROUP BY Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 401_TrabajaActivoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_401_TrabajaActivoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CI
FROM Trabaja
WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.FechaBaja) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 403_AportesUlt12xCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_403_AportesUlt12xCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.AnioMes
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Imponible.Concepto)='1') AND ((Imponible.CI)=@pCI) AND ((Imponible.AnioMes)>=(YEAR(DATEADD(month,13,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,13,CAST(GETDATE() AS date))))) AND ((Trabaja.FechaBaja) Is Null))
GROUP BY Imponible.CI, Imponible.AnioMes
)
GO

-- ===== DATA OBJECT FOR QUERY: 403_UltProxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_403_UltProxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Max(DATEADD(day,[CertificacionProrroga].[Dias],[CertificacionProrroga].[Fecha])) AS Fecha
FROM CertificacionProrroga
WHERE (((CertificacionProrroga.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 450_AfiliadoMutualista =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_450_AfiliadoMutualista_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.CodMutualista, Mutualista.Cuota
FROM Afiliado INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
WHERE (((Afiliado.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_AdPreJubxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_AdPreJubxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT AdPreJub.*
FROM AdPreJub
WHERE (((AdPreJub.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_AfiliadoCertificadoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_AfiliadoCertificadoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT TOP 1 Certificacion.CI
FROM Certificacion
WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.Efectiva)= 1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_AfiliadoSubsidioxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_AfiliadoSubsidioxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT TOP 1 SubsidioCabezal.CI
FROM SubsidioCabezal
WHERE (((SubsidioCabezal.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_Imponible =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_460_Imponible_q]
AS
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Imponible.Concepto)='1') AND ((Imponible.AnioMes)>=(YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed]))))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 460_IMSxAnioMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_IMSxAnioMes_q](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT IMS.Importe
FROM IMS
WHERE (((IMS.AnioMes)=@pAnioMes))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_IMS_Actual =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_IMS_Actual_q](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT IMS.Importe
FROM IMS
WHERE (((IMS.AnioMes)=@pAnioMes))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_RegimenJubilatorioxCod =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_RegimenJubilatorioxCod_q](@pCodRegimenJubilatorio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT RegimenJubilatorio.*
FROM RegimenJubilatorio
WHERE (((RegimenJubilatorio.CodRegimenJubilatorio)=@pCodRegimenJubilatorio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_TrabajaActivoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_TrabajaActivoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CodEmpresa
FROM Trabaja
WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.FechaBaja) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_UltSMN =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_UltSMN_q](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.Importe, I.AnioMes
FROM IMS AS I
WHERE (((I.AnioMes) In (SELECT  MAX(AnioMes) FROM IMS AS I2
WHERE AnioMes <= @pAnioMes)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 460_UltSubsidioxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_UltSubsidioxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT S.CI, Sum(S.ValorJornal*30) AS Importe
FROM SubsidioCabezal AS S
WHERE (((S.CI)=@pCI) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[S].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [S].[Mes]), 2)))>=All (SELECT MAX(TRY_CONVERT(float,CONVERT(nvarchar(max),S1.Anio) + RIGHT('00' + CONVERT(varchar(2), S1.Mes), 2))) FROM SubsidioCabezal S1
WHERE S.CI = S1.CI)))
GROUP BY S.CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 470_AdPreJubPagoxCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_470_AdPreJubPagoxCI_q](@pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT AdPreJubPago.*, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
FROM AdPreJubPago INNER JOIN Afiliado ON AdPreJubPago.CI = Afiliado.CI
WHERE (((AdPreJubPago.CI)=@pCI) AND ((AdPreJubPago.Fecha) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 470_AdPreJubPagoxCI-Mes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_470_AdPreJubPagoxCI_Mes_q](@pCI INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT AdPreJubPago.*, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
FROM AdPreJubPago INNER JOIN Afiliado ON AdPreJubPago.CI = Afiliado.CI
WHERE (((AdPreJubPago.Mes)=@pMes) AND ((AdPreJubPago.Anio)=@pAnio) AND ((AdPreJubPago.CI)=@pCI) AND ((AdPreJubPago.Fecha) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 480_F_Ult_Certif =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_F_Ult_Certif_q]
AS
SELECT Certificacion.CI, Max(Certificacion.FechaFin) AS F_Ult_Certificacion
FROM Certificacion
WHERE (((Certificacion.Efectiva)= 1))
GROUP BY Certificacion.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 480_SumaProrrogas =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_SumaProrrogas_q]
AS
SELECT CertificacionProrroga.CI, Sum(CertificacionProrroga.Dias) AS Dias
FROM CertificacionProrroga
GROUP BY CertificacionProrroga.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 490_Subsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_490_Subsidio_q]
AS
SELECT *
FROM SubsidioCabezal
WHERE (( [Mes] = 7 And [Anio] = 2007 And [Liquidar] = 1 And CI = 8142038 ));
GO

-- ===== DATA OBJECT FOR QUERY: 500_Prorrogas =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Prorrogas_q]
AS
SELECT CertificacionProrroga.CI, Sum(CertificacionProrroga.Dias) AS Dias, Max(DATEADD(day, CertificacionProrroga.Dias, CertificacionProrroga.Fecha)) AS Fecha
FROM CertificacionProrroga
GROUP BY CertificacionProrroga.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Aporte_Tmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Aporte_Tmp_q]
AS
SELECT Imponible.Mes, Imponible.Anio, RIGHT('00' + CONVERT(varchar(2), Imponible.Mes), 2) + '/' + Imponible.Anio AS MesFormat, Empresa.CodEmpresa, Empresa.Nombre AS DescEmpresa, (CASE WHEN Imponible.Concepto='1' THEN Imponible.Importe ELSE 0 END) AS ImporteAporte, (CASE WHEN Imponible.Concepto='2' THEN Imponible.Importe ELSE 0 END) AS ImporteAguinaldo, (CASE WHEN Imponible.Importe=0 And Imponible.Concepto='1' THEN 1 ELSE NULL END) AS CantCero, Empresa.AporteCasemed, Empresa.AporteAguinaldo, Imponible.Importe, Imponible.CI
FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa
WHERE Imponible.CodEmpresa = 900 AND Imponible.Mes = 02 AND Imponible.Anio = 2008;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Cargado_HL_Det =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Cargado_HL_Det_q]
AS
SELECT Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Sum((CASE WHEN Concepto='1' THEN Imponible.Importe ELSE 0 END)) AS Sueldo, Sum((CASE WHEN Concepto='2' THEN Imponible.Importe ELSE 0 END)) AS Aguinaldo, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio
FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Imponible ON (Trabaja.FechaIngreso = Imponible.Fechaingreso) AND (Trabaja.CodEmpresa = Imponible.CodEmpresa) AND (Afiliado.CI = Imponible.CI)
GROUP BY Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END), Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Certificacion_UltFecha =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Certificacion_UltFecha_q]
AS
SELECT Certificacion.CI, Max(Certificacion.FechaFin) AS FechaFin
FROM Certificacion
GROUP BY Certificacion.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_DetalleSubsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio_q]
AS
SELECT SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioImponible.Mes, SubsidioImponible.Anio, SubsidioImponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, SubsidioImponible.Dias, SubsidioImponible.Importe, SubsidioCabezal.Mes AS MesCabezal, SubsidioCabezal.Anio AS AnioCabezal, SubsidioCabezal.Dias AS DiasSubsidio, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio
FROM (SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN (SubsidioImponible INNER JOIN Empresa ON SubsidioImponible.CodEmpresa = Empresa.CodEmpresa) ON SubsidioCabezal.IdSubsidio = SubsidioImponible.IdSubsidio;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Imponible_Tmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Imponible_Tmp_q]
AS
SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Afiliado.FechaNacimiento
FROM ((Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI) INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Trabaja.FechaBaja) Is Null) AND ((((TRY_CONVERT(int,[Imponible].[Anio]) * 100) + TRY_CONVERT(int,[Imponible].[Mes]))) Between 200507 And 201404))
 AND Imponible.CI = 11127168;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_NoCargadoHL =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_NoCargadoHL_q]
AS
SELECT Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + '' <> '' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
FROM Afiliado
WHERE (((Afiliado.CI) NOT In (Select CI FROM Imponible Where [CodEmpresa]  = 900  And [Mes] = 05 And [Anio] = 2013)  And (Afiliado.CI) In (SELECT CI From Trabaja Where [CodEmpresa] = 900 And ([FechaBaja] Is Null Or (YEAR([FechaBaja]) * 100 + MONTH([FechaBaja])) >= 201305))));
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Prestacion =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Prestacion_q]
AS
SELECT Prestacion.CI, Prestacion.Fecha, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Prestacion.CodPrestacionTipo, PrestacionTipo.Descrip AS DescPrestacionTipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta, Prestacion.Observaciones, Prestacion.Usr, Prestacion.Ts
FROM (Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_PrimaFallecimiento_Tmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_PrimaFallecimiento_Tmp_q]
AS
SELECT Afiliado.CI, YEAR([FechaFallecimiento]) AS Anio, MONTH([FechaFallecimiento]) AS Mes, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, PrimaFallecimiento.Importe AS ImpNominal, 0 AS Importe, PrimaFallecimiento.FechaFallecimiento, TRY_CONVERT(datetime2,'01/' + FORMAT(DATEADD(month,-6,PrimaFallecimiento.FechaFallecimiento),'mm/yyyy')) AS FechaIni, DATEADD(day,-1,TRY_CONVERT(datetime2,'01/' + FORMAT(PrimaFallecimiento.FechaFallecimiento,'mm/yyyy'))) AS FechaFin
FROM Afiliado INNER JOIN PrimaFallecimiento ON Afiliado.CI = PrimaFallecimiento.CI
WHERE (((PrimaFallecimiento.FechaFallecimiento) Is NOT Null)) AND Afiliado.[CI]= 18752605;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_ReintegroMutual =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_ReintegroMutual_q]
AS
SELECT ReintegroMutual.CI, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, ReintegroMutual.Mes, ReintegroMutual.Anio, ReintegroMutual.Fecha, ReintegroMutual.CodMutualista, Mutualista.Descrip AS DescMutualista, ReintegroMutual.Importe, ReintegroMutual.Observaciones, ReintegroMutual.Usr, ReintegroMutual.Ts
FROM (ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI) INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista
WHERE ( [ReintegroMutual].[Mes] = 7 and [ReintegroMutual].[Anio] = 2003 );
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_ResumenSubsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_ResumenSubsidio_q]
AS
SELECT SubsidioCabezal.Anio, SubsidioCabezal.Mes, (CASE WHEN LEN(CONVERT(nvarchar(max),SubsidioCabezal.CI))>7 THEN FORMAT(SubsidioCabezal.CI,'@\.@@@\.@@@-@') ELSE FORMAT(SubsidioCabezal.CI,'@@@\.@@@-@') END) AS CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.Dias, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioCabezal.Liquidar, Afiliado.FechaNacimiento, [600_SubsidioFecha_Tmp].DescFecha, SubsidioCabezal.CI AS CIOrig
FROM (SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) LEFT JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio
WHERE (((SubsidioCabezal.ImpLiquido)>0));
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Trabaja_Tmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Trabaja_Tmp_q]
AS
SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Trabaja.FechaBaja) Is Null) AND ((Empresa.Ficticia)= 0)) AND Afiliado.[CI]= 36168331;
GO

-- ===== DATA OBJECT FOR QUERY: 506_Rpt_LiquidacionBPS =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes INT, @pAnio INT)
RETURNS TABLE
AS
RETURN
(
SELECT Liquidacion_BPS.*
FROM Liquidacion_BPS
WHERE (((Liquidacion_BPS.MES)=@pMes) AND ((Liquidacion_BPS.ANIO)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 506_Rpt_Subsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_506_Rpt_Subsidio_q]
AS
SELECT SubsidioCabezal.CI, SubsidioCabezal.Dias, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, SubsidioCabezal_BPS.IdSubsidio, SubsidioCabezal.NroRecibo, SubsidioEnfermedad.FechaIni, SubsidioEnfermedad.FechaFin, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, (CASE WHEN [SubsidioCabezal].Dias>0 THEN [SubsidioCabezal].[ImpNominal]/SubsidioCabezal.Dias*0.7 ELSE 0 END) AS Jornal70, (CASE WHEN [SubsidioCabezal].Dias>0 THEN SubsidioCabezal.ImpAguinaldo/SubsidioCabezal.Dias*0.7 ELSE 0 END) AS Aguinaldo70, SubsidioCabezal_BPS.DiasBPS, SubsidioCabezal_BPS.LiquidoBPS, SubsidioCabezal_BPS.LiquidoPagar, Banco.Descripcion AS Banco, Afiliado.NroCuenta, SubsidioCabezal.Mes, SubsidioCabezal.Anio, Afiliado.EMail
FROM (SubsidioEnfermedad INNER JOIN ((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) LEFT JOIN SubsidioCabezal_BPS ON SubsidioCabezal.IdSubsidio = SubsidioCabezal_BPS.IdSubsidio) ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco;
GO

-- ===== DATA OBJECT FOR QUERY: 506_TotalLiquidoBPSCIMes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_506_TotalLiquidoBPSCIMes_q](@pCI INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Sum(SubsidioCabezal_BPS.LiquidoBPS) AS LiquidoBPS
FROM SubsidioCabezal INNER JOIN SubsidioCabezal_BPS ON SubsidioCabezal.IdSubsidio = SubsidioCabezal_BPS.IdSubsidio
WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 601_Rpt_Recibo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_601_Rpt_Recibo_q]
AS
SELECT [600_Rpt_Recibo].*, SubsidioCabezal.FechaPago
FROM [600_Rpt_Recibo] INNER JOIN SubsidioCabezal ON [600_Rpt_Recibo].IdSubsidio = SubsidioCabezal.IdSubsidio;
GO

-- ===== DATA OBJECT FOR QUERY: 765_CertificacionContinua =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_765_CertificacionContinua_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT DISTINCT Certificacion.CI
FROM Certificacion
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaIni)<=@pFecha) AND ((Certificacion.FechaFin)>@pFecha))
)
GO

-- ===== DATA OBJECT FOR QUERY: 765_CertificacionEmpalma =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_765_CertificacionEmpalma_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT DISTINCT Certificacion.CI
FROM Certificacion
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaIni)=DATEADD(day, 1, @pFecha)))
and Certificacion.CI In (select CI from certificacion where efectiva= 1 and FechaFin=@pFecha)
)
GO

-- ===== DATA OBJECT FOR QUERY: 800_AfiliadoImponible_Mes =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Imponible.Concepto)='1') AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio
)
GO

-- ===== DATA OBJECT FOR QUERY: 800_AfiliadoImponible_Mes_Fecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa INT, @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
WHERE (((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1') AND (((YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed])))<=@pMes) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END))) OR (((Imponible.Concepto)='1') AND (((YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed])))<=@pMes) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)) AND (((YEAR([Trabaja].[FechaBaja]) * 100 + MONTH([Trabaja].[FechaBaja])))>@pMes))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio
)
GO

-- ===== DATA OBJECT FOR QUERY: 800_AfiliadoImponible_Mes_Todos =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Todos_q]
AS
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible
WHERE (((Imponible.Concepto)='1'))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 800_Afiliado_Imponible_Mes_Fecha =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_800_Afiliado_Imponible_Mes_Fecha_q](@pCodEmpresa INT, @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
WHERE (((Imponible.Concepto)='1') AND (((Trabaja.FechaBaja) Is Null) Or (YEAR(Trabaja.FechaBaja) * 100 + MONTH(Trabaja.FechaBaja)) > @pMes)
AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio
)
GO

-- ===== DATA OBJECT FOR QUERY: 800_Cantidad_Empresa =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_800_Cantidad_Empresa_q](@pCodEmpresa INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Trabaja.CodEmpresa)=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 800_Cantidad_Otras =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_800_Cantidad_Otras_q](@pCodEmpresa INT, @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.CodEmpresa)<>(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 805_Activos =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_805_Activos_q]
AS
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (Afiliado.CI) In (Select CI From Trabaja Where FechaBaja is Null);
GO

-- ===== DATA OBJECT FOR QUERY: 805_CertificacionesxAnio =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_805_CertificacionesxAno_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT YEAR(Certificacion.FechaCertificacion) AS Anio, Count(*) AS Cantidad
FROM Certificacion
WHERE (((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((Certificacion.Efectiva)= 1))
GROUP BY YEAR(Certificacion.FechaCertificacion)
)
GO

-- ===== DATA OBJECT FOR QUERY: 805_CertificadosActivos =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_805_CertificadosActivos_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)
)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 805_CertificadosActivos_Original =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_805_CertificadosActivos_Original_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) And (Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)
)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 805_Certificados_AnioCI =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_805_Certificados_AnoCI_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT DISTINCT Certificacion.CI, YEAR(Certificacion.FechaCertificacion) AS Anio
FROM Certificacion
WHERE (((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((Certificacion.Efectiva)= 1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 806_CertificadosEntre =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_806_CertificadosEntre_q](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 806_CertificadosMayores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_806_CertificadosMayores_q](@pAnioIni NVARCHAR(MAX), @pFechaIni NVARCHAR(MAX), @pFechaFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 806_CertificadosMenores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_806_CertificadosMenores_q](@pAnioIni NVARCHAR(MAX), @pFechaIni NVARCHAR(MAX), @pFechaFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 806_CertificadosSexo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_806_CertificadosSexo_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad, Afiliado.Sexo
FROM Afiliado
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))))
GROUP BY Afiliado.Sexo
HAVING (((Afiliado.Sexo) Is NOT Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: 807_CertificadosEspecialidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_807_CertificadosEspecialidad_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))))
GROUP BY AfiliadoEspecialidad.CodEspecialidad
)
GO

-- ===== DATA OBJECT FOR QUERY: 808_CertificadosAfecciones =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_808_CertificadosAfecciones_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia INT)
RETURNS TABLE
AS
RETURN
(
SELECT Certificacion.CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)= (CASE WHEN @pCodPatologia is NOT Null THEN @pCodPatologia ELSE AfeccionGrupo.CodPatologia END)  ))
GROUP BY Certificacion.CodAfeccionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 808_Certificados_Cantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_808_Certificados_Cantidad_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Certificacion
WHERE (((Certificacion.Efectiva)= 1) AND FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))
)
GO

-- ===== DATA OBJECT FOR QUERY: 809_AfiliadoActivoFecha_Cantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_809_AfiliadoActivoFecha_Cantidad_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaIngCasemed <= @pFecha And
((FechaBaja Is Null Or FechaBaja > @pFecha) AND
FechaIngreso <= @pFecha))))
)
GO

-- ===== DATA OBJECT FOR QUERY: 809_AfiliadoActivo_Cantidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_809_AfiliadoActivo_Cantidad_q]
AS
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja where FechaIngCasemed<= CAST(GETDATE() AS date) And (fechabaja is null Or FechaBaja > CAST(GETDATE() AS date)))));
GO

-- ===== DATA OBJECT FOR QUERY: 809_AfiliadoFecha_Cantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_809_AfiliadoFecha_Cantidad_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (SELECT CI FROM TRABAJA WHERE FechaIngreso <= @pFecha)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 809_Afiliado_Cantidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_809_Afiliado_Cantidad_q]
AS
SELECT Count(*) AS Cantidad
FROM Afiliado;
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadoActivoEntre =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadoActivoEntre_q](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadoActivoMayores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadoActivoMayores_q](@pAnioIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadoActivoMenores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadoActivoMenores_q](@pAnioIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosActivoSexo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_810_AfiliadosActivoSexo_q]
AS
SELECT Afiliado.Sexo, Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI FROM Trabaja WHERE FechaBaja is Null)))
GROUP BY Afiliado.Sexo;
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosEntre =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosEntre_q](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha)) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosMayores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosMayores_q](@pAnioIni NVARCHAR(MAX), @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosMenores =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosMenores_q](@pAnioIni NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha)) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosSexo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosSexo_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.Sexo, Count(*) AS Cantidad
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI FROM Trabaja WHERE (FechaBaja is Null Or FechaBaja > @pFecha) And FechaIngCasemed <= @pFecha)))
GROUP BY Afiliado.Sexo
)
GO

-- ===== DATA OBJECT FOR QUERY: 812_AfiliadoActivoEspecialidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_812_AfiliadoActivoEspecialidad_q]
AS
SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null)))
GROUP BY AfiliadoEspecialidad.CodEspecialidad;
GO

-- ===== DATA OBJECT FOR QUERY: 812_AfiliadosEspecialidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_812_AfiliadosEspecialidad_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngCasemed <= @pFecha)))
GROUP BY AfiliadoEspecialidad.CodEspecialidad
)
GO

-- ===== DATA OBJECT FOR QUERY: 813_CertificacionAfeccionDistintas =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_813_CertificacionAfeccionDistintas_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT DISTINCT Certificacion.CI, Certificacion.CodAfeccionTipo
FROM Certificacion
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 816_Certificados_GrupoAfeccion =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_816_Certificados_GrupoAfeccion_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT AfeccionGrupo.CodAfeccionGrupo AS Codigo, MIN(AfeccionGrupo.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
GROUP BY AfeccionGrupo.CodAfeccionGrupo
)
GO

-- ===== DATA OBJECT FOR QUERY: 817_Certificados_Patologia =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_817_Certificados_Patologia_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descripcion, Count(*) AS Cantidad
FROM ((AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
GROUP BY Patologia.CodPatologia
)
GO

-- ===== DATA OBJECT FOR QUERY: 818_Certificados_Patologia =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_818_Certificados_Patologia_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descripcion, Sum(DATEDIFF(day, Certificacion.FechaIni, Certificacion.FechaFin) + 1) AS Cantidad
FROM ((AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
GROUP BY Patologia.CodPatologia
)
GO

-- ===== DATA OBJECT FOR QUERY: 819_Certificados_AfeccionGrupo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_819_Certificados_AfeccionGrupo_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT AfeccionGrupo.CodAfeccionGrupo AS Codigo, MIN(AfeccionGrupo.Descrip) AS Descrip, Sum(DATEDIFF(day,Certificacion.FechaIni,Certificacion.FechaFin)+1) AS Cantidad
FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
GROUP BY AfeccionGrupo.CodAfeccionGrupo
)
GO

-- ===== DATA OBJECT FOR QUERY: 820_Certificados_AfeccionTipo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_820_Certificados_AfeccionTipo_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT AfeccionTipo.CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descrip, Sum(DATEDIFF(day,Certificacion.FechaIni,Certificacion.FechaFin)+1) AS Cantidad
FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND (([AfeccionGrupo].[CodPatologia])=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
GROUP BY AfeccionTipo.CodAfeccionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 822_AfiliadoGE =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_822_AfiliadoGE_q]
AS
SELECT Afiliado.CI, DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END) AS FechaHoy, DATEDIFF(year,FechaNacimiento,CAST(GETDATE() AS date))-(CASE WHEN DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END)<=CAST(GETDATE() AS date) THEN 0 ELSE 1 END) AS Edad, Afiliado.FechaNacimiento, Afiliado.Sexo
FROM Afiliado
WHERE (((Afiliado.FechaNacimiento) Is NOT Null));
GO

-- ===== DATA OBJECT FOR QUERY: 824_PrestacionesCantidad =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_824_PrestacionesCantidad_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Count(*) AS Cantidad
FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)))
GROUP BY Prestacion.CodPrestacionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 825_PrestacionesImporte_Pesos =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_825_PrestacionesImporte_Pesos_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Sum(Prestacion.Importe) AS Importe
FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)) AND ((Prestacion.Moneda)='$'))
GROUP BY Prestacion.CodPrestacionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 826_PrestacionesImporte_Dolares =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_826_PrestacionesImporte_Dolares_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Sum(Prestacion.Importe) AS Importe
FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)) AND ((Prestacion.Moneda)='U$S'))
GROUP BY Prestacion.CodPrestacionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 828_Cantidad_Empresa =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_828_Cantidad_Empresa_q](@pCodEmpresa NVARCHAR(MAX), @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Trabaja.CodEmpresa)=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND (EXISTS(SELECT * FROM Imponible Where Trabaja.CI = Imponible.CI And Trabaja.FechaIngreso = Imponible.FechaIngreso And Imponible.CodEmpresa = Trabaja.CodEmpresa AND Concepto = '1' AND AnioMes = (CASE WHEN @pFecha IS NULL THEN (YEAR(DATEADD(month, -2, CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))) ELSE (YEAR(DATEADD(month, -2, @pFecha)) * 100 + MONTH(DATEADD(month, -2, @pFecha))) END) And Importe > 0
  )))
)
GO

-- ===== DATA OBJECT FOR QUERY: 828_Cantidad_Otras =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_828_Cantidad_Otras_q](@pCodEmpresa INT, @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.CodEmpresa)<>(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND (EXISTS(SELECT * FROM Imponible Where Trabaja.CI = Imponible.CI And Trabaja.FechaIngreso = Imponible.FechaIngreso And Imponible.CodEmpresa = Trabaja.CodEmpresa AND Concepto = '1' AND AnioMes = (CASE WHEN @pFecha IS NULL THEN (YEAR(DATEADD(month, -2, CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))) ELSE (YEAR(DATEADD(month, -2, @pFecha)) * 100 + MONTH(DATEADD(month, -2, @pFecha))) END) And Importe > 0
  )))
)
GO

-- ===== DATA OBJECT FOR QUERY: 830_CantidadPorPuesto =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_830_CantidadPorPuesto_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CodEmpresa, Count(Trabaja.CI) AS Cantidad
FROM Trabaja
WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>@pFecha))
GROUP BY Trabaja.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 830_CantidadPorPuestoNo0 =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_830_CantidadPorPuestoNo0_q](@pFecha DATETIME2(0), @pMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT t.CodEmpresa, Count(t.CI) AS Cantidad
FROM Trabaja AS t
WHERE (((t.FechaBaja) Is Null Or (t.FechaBaja)>@pFecha)) AND EXISTS (SELECT 1 FROM Imponible i WHERE t.CI=i.CI and i.CodEmpresa = t.CodEmpresa AND i.AnioMes = @pMes AND i.Importe>0 And i.Concepto='1')
GROUP BY t.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 999_Excel_Tmp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_999_Excel_Tmp_q]
AS
SELECT *
FROM Rpt_Historia_Vandalismo_S;
GO

-- ===== DATA OBJECT FOR QUERY: 999_Parametros =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_999_Parametros_q](@pLogin NVARCHAR(MAX), @pClave NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT xPar.login, xPar.Clave, xPar.orden, xPar.value1, xPar.value2, xPar.value3, xPar.value4, xPar.value5
FROM xUsrParam AS xPar
WHERE (((xPar.login)=@pLogin) AND ((xPar.Clave)=@pClave))
)
GO

-- ===== DATA OBJECT FOR QUERY: BpsMutualista =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_BpsMutualista_q]
AS
SELECT BpsFormat.Mutualista
FROM BpsFormat
GROUP BY BpsFormat.Mutualista;
GO

-- ===== DATA OBJECT FOR QUERY: Buscar duplicados por Bps4 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Buscar_duplicados_por_Bps4_q]
AS
SELECT DISTINCT [CI], [TipoReg], [AcumulacionLaboral], [Concepto], [Imponible]
FROM Bps4
WHERE [CI] In (SELECT [CI] FROM [Bps4] As Tmp GROUP BY [CI] HAVING Count(*)>1 );
GO

-- ===== DATA OBJECT FOR QUERY: Buscar duplicados por zRs_AEsp =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Buscar_duplicados_por_zRs_AEsp_q]
AS
SELECT DISTINCT [CI], [EspNom1], [EspNom2], [EspNom3]
FROM zRs_AEsp
WHERE [CI] In (SELECT [CI] FROM [zRs_AEsp] As Tmp GROUP BY [CI] HAVING Count(*)>1 );
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Afiliado =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Afiliado_q]
AS
SELECT Afiliado.CI, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Mutualista.Descrip AS DescMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, RegimenJubilatorio.Descrip AS DescRegimenJubilatorio, Afiliado.Usr, Afiliado.Ts, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Mutualista.Cuota, Afiliado.Direccion, Afiliado.PagaMutualista, Afiliado.CodDepartamento, Departamento.Descrip AS DescDepartamento, Afiliado.Movil
FROM ((Afiliado INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista) INNER JOIN RegimenJubilatorio ON Afiliado.CodRegimenJubilatorio = RegimenJubilatorio.CodRegimenJubilatorio) LEFT JOIN Departamento ON Afiliado.CodDepartamento = Departamento.CodDepartamento;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Aporte =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Aporte_q]
AS
SELECT Imponible.Mes, Imponible.Anio, RIGHT('00' + CONVERT(varchar(2), Imponible.Mes), 2) + '/' + Imponible.Anio AS MesFormat, Empresa.CodEmpresa, Empresa.Nombre AS DescEmpresa, (CASE WHEN Imponible.Concepto='1' THEN Imponible.Importe ELSE 0 END) AS ImporteAporte, (CASE WHEN Imponible.Concepto='2' THEN Imponible.Importe ELSE 0 END) AS ImporteAguinaldo, (CASE WHEN Imponible.Importe=0 And Imponible.Concepto='1' THEN 1 ELSE NULL END) AS CantCero, Empresa.AporteCasemed, Empresa.AporteAguinaldo, Imponible.Importe, Imponible.CI
FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Certificacion =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Certificacion_q]
AS
SELECT Certificacion.NroLlamado, Certificacion.CI, Certificacion.NroRecibo, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.CodAfeccionTipo, AfeccionTipo.Descrip AS DescAfeccionTipo, Certificacion.CodCertificador, Certificador.Descrip AS DescCertificador, Certificacion.CodSalidaTipo, SalidaTipo.Descrip AS DescSalidaTipo, Certificacion.Efectiva, Certificacion.Indicaciones, Certificacion.ImporteDeducible, Certificacion.Usr, Certificacion.Ts, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado
FROM (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Certificacion2 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Certificacion2_q]
AS
SELECT Certificacion.NroLlamado, (CASE WHEN LEN(Certificacion.CI)>=8 THEN FORMAT(Certificacion.CI, '@.@@@.@@@-@') ELSE FORMAT(Certificacion.CI, '@@@.@@@-@') END) AS CI, Certificacion.NroRecibo, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.CodAfeccionTipo, AfeccionTipo.Descrip AS DescAfeccionTipo, Certificacion.CodCertificador, Certificador.Descrip AS DescCertificador, Certificacion.CodSalidaTipo, Certificacion.Efectiva, Certificacion.Indicaciones, Certificacion.ImporteDeducible, Certificacion.Usr, Certificacion.Ts, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado
FROM ((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Discount =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_Rpt_Discount_q](@pCodDiscount NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pFecha DATETIME2(0), @pCodBanco NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.NroFunCuenta, Afiliado.NroCuenta, @pFecha AS Fecha, SubsidioCabezal.ImpLiquido
FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((Afiliado.CodBanco)=@pCodBanco))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Imponible =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Imponible_q]
AS
SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe
FROM (Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Imponible_Activo =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_Rpt_Imponible_Activo_q](@pMesIni NVARCHAR(MAX), @pMesFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Afiliado.FechaNacimiento
FROM ((Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI) INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
WHERE (((Trabaja.FechaBaja) Is Null) AND ((((TRY_CONVERT(int,[Imponible].[Anio]) * 100) + TRY_CONVERT(int,[Imponible].[Mes]))) Between @pMesIni And @pMesFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Mutualista =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Mutualista_q]
AS
SELECT Mutualista.CodMutualista, Mutualista.Descrip, Mutualista.Direccion, Mutualista.Telefono, Mutualista.Fax, Mutualista.EMail, Mutualista.DiaPago, Mutualista.CodFormaPago, FormaPago.Descrip AS DescFormaPago, Mutualista.PersonaContacto, Mutualista.Cuota, Mutualista.Usr, Mutualista.Ts
FROM Mutualista INNER JOIN FormaPago ON Mutualista.CodFormaPago = FormaPago.CodFormaPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Prestacion =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Prestacion_q]
AS
SELECT Prestacion.CI, Prestacion.Fecha, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Prestacion.CodPrestacionTipo, PrestacionTipo.Descrip AS DescPrestacionTipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta, Prestacion.Observaciones, Prestacion.Usr, Prestacion.Ts
FROM (Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_ReintegroMutual =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_ReintegroMutual_q]
AS
SELECT ReintegroMutual.CI, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, ReintegroMutual.Mes, ReintegroMutual.Anio, ReintegroMutual.Fecha, ReintegroMutual.CodMutualista, Mutualista.Descrip AS DescMutualista, ReintegroMutual.Importe, ReintegroMutual.Observaciones, ReintegroMutual.Usr, ReintegroMutual.Ts
FROM (ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI) INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_SubsidioCabezal =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_SubsidioCabezal_q]
AS
SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, SubsidioCabezal.Liquidar, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioCabezal.NroRecibo, SubsidioCabezal.FechaPago, SubsidioCabezal.Usr, SubsidioCabezal.Ts, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, [600_SubsidioFecha_Tmp].DescFecha, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta, Afiliado.NroFunCuenta, Afiliado.CodBanco
FROM ((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Trabaja =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Trabaja_q]
AS
SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa
FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Trabaja.FechaBaja) Is Null) AND ((Empresa.Ficticia)= 0));
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Trabaja_Rpt =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Trabaja_Rpt_q]
AS
SELECT Afiliado.CI, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, Trabaja.FechaBaja FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: rsAfiliadoActivo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_rsAfiliadoActivo_q]
AS
SELECT Afiliado.*
FROM Afiliado
WHERE (((Afiliado.CI) In (select ci from trabaja where fechabaja is null)));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfeccionGrupo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfeccionGrupo_q]
AS
SELECT AfeccionGrupo.*
FROM AfeccionGrupo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfeccionGrupo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfeccionGrupo_Desc_q]
AS
SELECT AfeccionGrupo.CodAfeccionGrupo, AfeccionGrupo.Descrip
FROM AfeccionGrupo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfeccionTipo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfeccionTipo_q]
AS
SELECT AfeccionTipo.*
FROM AfeccionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfeccionTipo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfeccionTipo_Desc_q]
AS
SELECT AfeccionTipo.CodAfeccionTipo, AfeccionTipo.Descrip
FROM AfeccionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Afiliado =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Afiliado_q]
AS
SELECT Afiliado.*
FROM Afiliado;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoApunte =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoApunte_q]
AS
SELECT AfiliadoApunte.CI, AfiliadoApunte.Fecha, AfiliadoApunte.Descrip, AfiliadoApunte.Usr, AfiliadoApunte.Ts
FROM AfiliadoApunte;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoApunteFromPeriodo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoApunteFromPeriodo_q]
AS
SELECT afiliado.ci, nombres, apellido1, apellido2, fecha, descrip
FROM afiliado INNER JOIN afiliadoapunte ON afiliado.ci=afiliadoapunte.ci;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoCristalin =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoCristalin_q]
AS
SELECT Afiliado.*
FROM Afiliado
WHERE (((Afiliado.CI) In (Select CI From Trabaja where CodEmpresa=7)));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoEspecialidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad_q]
AS
SELECT AfiliadoEspecialidad.*
FROM AfiliadoEspecialidad;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoEspecialidadDesc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoEspecialidadDesc_q]
AS
SELECT AfiliadoEspecialidad.CI, Especialidad.Descrip
FROM AfiliadoEspecialidad INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoEspecialidad_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad_Desc_q]
AS
SELECT AfiliadoEspecialidad.CI, AfiliadoEspecialidad.CodEspecialidad
FROM AfiliadoEspecialidad;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoImponibleMes =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoImponibleMes_q]
AS
SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe, MIN(Imponible.AnioMes) AS AnioMes
FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
WHERE (((Imponible.Concepto)='1') AND ((Trabaja.FechaBaja) Is Null))
GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AfiliadoImponibleMesNoBaja =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]
AS
SELECT Imponible.CI, Imponible.AnioMes, Sum(Imponible.Importe) AS Importe, MIN(Imponible.AnioMes) AS FirstOfAnioMes
FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
WHERE (((Imponible.Concepto)='1'))
GROUP BY Imponible.CI, Imponible.AnioMes;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Afiliado_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Afiliado_Desc_q]
AS
SELECT Afiliado.CI, [Nombres] + ' ' + [Apellido1] + ' ' + [Apellido2] AS Descrip
FROM Afiliado;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Afiliado_FechaNacimiento =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Afiliado_FechaNacimiento_q]
AS
SELECT Afiliado.*
FROM Afiliado
WHERE (((Afiliado.FechaNacimiento) Is NOT Null));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AporteTipo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AporteTipo_q]
AS
SELECT AporteTipo.*
FROM AporteTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_AporteTipo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_AporteTipo_Desc_q]
AS
SELECT AporteTipo.CodAporteTipo, AporteTipo.Descrip
FROM AporteTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_BajaMotivo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_BajaMotivo_q]
AS
SELECT BajaMotivo.*
FROM BajaMotivo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_BajaMotivo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_BajaMotivo_Desc_q]
AS
SELECT BajaMotivo.CodBajaMotivo, BajaMotivo.Descrip
FROM BajaMotivo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Banco_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Banco_Desc_q]
AS
SELECT Banco.CodBanco, Banco.Descripcion
FROM Banco;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Bps2 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Bps2_q]
AS
SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps2.CI)>0 THEN LEFT(Bps2.CI,CHARINDEX('-', Bps2.CI)-1) + SUBSTRING(Bps2.CI,CHARINDEX('-', Bps2.CI)+1,1) ELSE Bps2.CI END)) AS Cedula, Bps2.Apellido1, Bps2.Apellido2, Bps2.Nombres, Bps2.FechaNacimiento, Bps2.Sexo, Bps2.Nacionalidad, Bps2.Reservado1, Bps2.Reservado2, Bps2.Reservado3, Bps2.Reservado4
FROM Bps2;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Bps3 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Bps3_q]
AS
SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps3.CI)>0 THEN LEFT(Bps3.CI,CHARINDEX('-', Bps3.CI)-1) + SUBSTRING(Bps3.CI,CHARINDEX('-', Bps3.CI)+1,1) ELSE Bps3.CI END)) AS Cedula, Bps3.AcumulacionLaboral, Bps3.FechaIngreso, Bps3.SeguroSalud, Bps3.RemuneracionTipo, Bps3.HorasSemanales, Bps3.VinculoFuncional, Bps3.CodigoExoneracion, Bps3.ComputosEspeciales, Bps3.CausalBaja, Bps3.FechaBaja, Bps3.LocalEmpresa, Bps3.DiasTrabajados, Bps3.HorasTrabajadas, Bps3.Reservado1, Bps3.Reservado2, Bps3.Reservado3, Bps3.Reservado4, Bps3.Reservado5
FROM Bps3;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Bps4 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Bps4_q]
AS
SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps4.CI)>0 THEN LEFT(Bps4.CI,CHARINDEX('-', Bps4.CI)-1) + SUBSTRING(Bps4.CI,CHARINDEX('-', Bps4.CI)+1,1) ELSE Bps4.CI END)) AS Cedula, (CASE WHEN Bps4.Concepto='2' THEN '2' ELSE '1' END) AS Concepto, Bps4.Imponible
FROM Bps4
WHERE (((Bps4.Concepto)='1' Or (Bps4.Concepto)='2'));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_CaseCasm =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_CaseCasm_q]
AS
SELECT CONVERT(nvarchar(max),Casecasm.Campo1) AS CI, Casecasm.Campo2, Casecasm.Campo3, Casecasm.Campo4, Casecasm.Campo5
FROM Casecasm
WHERE (((Casecasm.Campo5) Like 'D.*'));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Certificacion =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Certificacion_q]
AS
SELECT Certificacion.*
FROM Certificacion;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Certificacion_Nombre =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Certificacion_Nombre_q]
AS
SELECT Certificacion.*, Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) + ', ' + Afiliado.Nombres AS Nombre
FROM Certificacion INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Certificador =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Certificador_q]
AS
SELECT Certificador.*
FROM Certificador;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Certificador_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Certificador_Desc_q]
AS
SELECT Certificador.CodCertificador, Certificador.Descrip
FROM Certificador;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Cristalin =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Cristalin_q]
AS
SELECT TRY_CONVERT(float,CONVERT(nvarchar(max),[Cristalin].[DOCUMENTO])) AS CI, Cristalin.[1ER APELLIDO], Cristalin.[2DO APELLIDO], Cristalin.[1ER NOMBRE], Cristalin.[2DO NOMBRE]
FROM Cristalin;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Departamento_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Departamento_Desc_q]
AS
SELECT Departamento.CodDepartamento, Departamento.Descrip
FROM Departamento;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Empleo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Empleo_q]
AS
SELECT Trabaja.CI, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, Trabaja.FechaIngreso, Trabaja.FechaIngCasemed, Trabaja.FechaBaja, Trabaja.CodBajaMotivo, Trabaja.NroFichaEmpresa, Trabaja.IdTrabaja, Trabaja.Usr, Trabaja.Ts
FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Empresa =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Empresa_q]
AS
SELECT Empresa.*
FROM Empresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_EmpresaPago =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_EmpresaPago_q]
AS
SELECT EmpresaPago.*
FROM EmpresaPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Empresa_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Empresa_Desc_q]
AS
SELECT Empresa.CodEmpresa, Empresa.Nombre
FROM Empresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Empresa_Desc_Real =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Empresa_Desc_Real_q]
AS
SELECT Empresa.CodEmpresa, Empresa.Nombre
FROM Empresa
WHERE (((Empresa.Ficticia)= 0));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Especialidad_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Especialidad_Desc_q]
AS
SELECT Especialidad.CodEspecialidad, Especialidad.Descrip
FROM Especialidad;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Export_BROU =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_Rs_Export_BROU_q](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.CI, SubsidioCabezal.ImpLiquido, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, Afiliado.CodBanco, Afiliado.NroCuenta, @pFecha AS Fecha
FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
WHERE (((Afiliado.CodBanco)=5) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.ImpLiquido)>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Export_NBC =====
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_Rs_Export_NBC_q](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioCabezal.CI, SubsidioCabezal.ImpLiquido, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, Afiliado.CodBanco, Afiliado.NroCuenta, @pFecha AS Fecha
FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
WHERE (((Afiliado.CodBanco)=1) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rs_FormaPago =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_FormaPago_q]
AS
SELECT FormaPago.*
FROM FormaPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_FormaPago_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_FormaPago_Desc_q]
AS
SELECT FormaPago.CodFormaPago, FormaPago.Descrip
FROM FormaPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_GrupoEtario_Descrip =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_GrupoEtario_Descrip_q]
AS
SELECT GrupoEtario.EdadIni, GrupoEtario.Descrip
FROM GrupoEtario;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_ImpMax =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_ImpMax_q]
AS
SELECT Imponible.CI, Max(Imponible.Importe) AS Importe
FROM Imponible
GROUP BY Imponible.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Imponible =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Imponible_q]
AS
SELECT Imponible.CI, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Fechaingreso, ([Imponible].[Anio] + '/' + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2)) AS MesDbg, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Imponible.IdTrabaja, Imponible.Mes, Imponible.Anio, Imponible.Usr, Imponible.Ts
FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Imponible_Ult =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Imponible_Ult_q]
AS
SELECT I.CI, I.CodEmpresa, I.Mes, I.Anio, I.Importe
FROM Imponible AS I
WHERE I.Concepto='1'
AND I.Mes = MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))
AND I.Anio = YEAR(DATEADD(month, -2, CAST(GETDATE() AS date)));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Imponible_Ult_Ant =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Imponible_Ult_Ant_q]
AS
SELECT I.CI, I.CodEmpresa, I.Mes, I.Anio, I.Importe
FROM Imponible AS I
WHERE (((I.Concepto)='1') AND (EXISTS(SELECT 1 FROM Imponible I1
WHERE I1.CI = I.CI AND I1.CodEmpresa = I.CodEmpresa AND I1.Concepto = '1'
GROUP BY I1.CI, I1.CodEmpresa
HAVING MAX(I1.ANIOMES) = I.AnioMes)));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_InformeEstadistico =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_InformeEstadistico_q]
AS
SELECT InformeEstadistico.*
FROM InformeEstadistico;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_MaxImp_Afiliado =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_MaxImp_Afiliado_q]
AS
SELECT Imponible.CI, MIN(Imponible.CodEmpresa) AS CodEmpresa, MIN(Imponible.Mes) AS Mes, MIN(Imponible.Anio) AS Anio, Max(Imponible.Importe) AS Importe
FROM Imponible
GROUP BY Imponible.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Mutualista =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Mutualista_q]
AS
SELECT Mutualista.*
FROM Mutualista
WHERE (((Mutualista.CodMutualista)>0));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Mutualista_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Mutualista_Desc_q]
AS
SELECT Mutualista.CodMutualista, Mutualista.Descrip
FROM Mutualista;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Patologia =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Patologia_q]
AS
SELECT Patologia.*
FROM Patologia;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Patologia_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Patologia_Desc_q]
AS
SELECT Patologia.CodPatologia, Patologia.Descrip
FROM Patologia;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_Prestacion =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_Prestacion_q]
AS
SELECT Prestacion.*, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
FROM Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestacionTipo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_PrestacionTipo_q]
AS
SELECT PrestacionTipo.*
FROM PrestacionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestacionTipo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_PrestacionTipo_Desc_q]
AS
SELECT PrestacionTipo.CodPrestacionTipo, PrestacionTipo.Descrip
FROM PrestacionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_RegimenAporte_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_RegimenAporte_Desc_q]
AS
SELECT RegimenAporte.CodRegimenAporte, RegimenAporte.Descrip
FROM RegimenAporte;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_RegimenJubilatorio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_RegimenJubilatorio_q]
AS
SELECT RegimenJubilatorio.*
FROM RegimenJubilatorio;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_RegimenJubilatorio_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_RegimenJubilatorio_Desc_q]
AS
SELECT RegimenJubilatorio.CodRegimenJubilatorio, RegimenJubilatorio.Descrip
FROM RegimenJubilatorio;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_ReintegroMutual =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_ReintegroMutual_q]
AS
SELECT ReintegroMutual.*, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
FROM ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SalidaTipo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SalidaTipo_q]
AS
SELECT SalidaTipo.*
FROM SalidaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SalidaTipo_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SalidaTipo_Desc_q]
AS
SELECT SalidaTipo.CodSalidaTipo, SalidaTipo.Descrip
FROM SalidaTipo;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SituacionMutual =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SituacionMutual_q]
AS
SELECT SituacionMutual.*
FROM SituacionMutual;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SituacionMutual_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SituacionMutual_Desc_q]
AS
SELECT SituacionMutual.CodSituacionMutual, SituacionMutual.Descrip
FROM SituacionMutual;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SituacionPago =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SituacionPago_q]
AS
SELECT SituacionPago.*
FROM SituacionPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SituacionPago_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SituacionPago_Desc_q]
AS
SELECT SituacionPago.CodSituacionPago, SituacionPago.Descrip
FROM SituacionPago;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SubsidioItem =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SubsidioItem_q]
AS
SELECT SubsidioItem.*, SubsidioItemCod.Tipo
FROM SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SubsidioItemCodXCI =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SubsidioItemCodXCI_q]
AS
SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod_Afiliado.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod_Afiliado.Vigencia AS FechaBaja, SubsidioItemCod.ModificaNominal, SubsidioItemCod_Afiliado.CI FROM SubsidioItemCod INNER JOIN SubsidioItemCod_Afiliado ON SubsidioItemCod.CodSubsidioItemCod = SubsidioItemCod_Afiliado.CodSubsidioItemCod;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SubsidioItemCod_Afiliado =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SubsidioItemCod_Afiliado_q]
AS
SELECT SubsidioItemCod_Afiliado.*, Afiliado.Apellido1 + ', ' + Afiliado.Nombres AS Nombre
FROM SubsidioItemCod_Afiliado INNER JOIN Afiliado ON SubsidioItemCod_Afiliado.CI = Afiliado.CI;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SubsidioItemCod_Desc =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SubsidioItemCod_Desc_q]
AS
SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip
FROM SubsidioItemCod;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_SubsidioXMes =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_SubsidioXMes_q]
AS
SELECT SubsidioCabezal.CI, SubsidioCabezal.Anio, SubsidioCabezal.Mes, Sum(SubsidioCabezal.ImpNominal) AS ImpNominal, Sum(SubsidioCabezal.ImpAguinaldo) AS ImpAguinaldo
FROM SubsidioCabezal
GROUP BY SubsidioCabezal.CI, SubsidioCabezal.Anio, SubsidioCabezal.Mes;
GO

-- ===== DATA OBJECT FOR QUERY: Rs_TrabajaActivo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_TrabajaActivo_q]
AS
SELECT Trabaja.*
FROM Trabaja
WHERE (((Trabaja.FechaBaja) Is Null));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_TrabajaUltimo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rs_TrabajaUltimo_q]
AS
SELECT Trabaja.CI, Trabaja.CodEmpresa, MAX(Trabaja.FechaIngreso) AS FechaIngreso, MAX(Trabaja.FechaBaja) AS FechaBaja, MAX(Trabaja.CodBajaMotivo) AS CodBajaMotivo, MAX(Trabaja.NroFichaEmpresa) AS NroFichaEmpresa, MAX(Trabaja.IdTrabaja) AS IdTrabaja, MAX(Trabaja.FechaIngCasemed) AS FechaIngCasemed, MAX(Trabaja.Usr) AS Usr, MAX(Trabaja.Ts) AS Ts
FROM Trabaja
GROUP BY Trabaja.CI, Trabaja.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xEmpresaCantidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xEmpresaCantidad_q]
AS
SELECT Trabaja.CodEmpresa, Count(*) AS Cantidad
FROM Trabaja
GROUP BY Trabaja.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xEmpresaPromedio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xEmpresaPromedio_q]
AS
SELECT Imponible.CodEmpresa, FORMAT(Avg(Imponible.Importe), '0.00') AS PromedioDeImporte
FROM Imponible
WHERE TRY_CONVERT(float,Anio + RIGHT('00' + CONVERT(varchar(2), Mes), 2)) Between 199906 And 199911
GROUP BY Imponible.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xEmpresaPromedioTodo =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xEmpresaPromedioTodo_q]
AS
SELECT Imponible.CodEmpresa, Avg(Imponible.Importe) AS PromedioDeImporte
FROM Imponible
GROUP BY Imponible.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xMutualistaCantidad =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xMutualistaCantidad_q]
AS
SELECT Afiliado.CodMutualista, Count(*) AS Cantidad
FROM Afiliado
GROUP BY Afiliado.CodMutualista;
GO

-- ===== DATA OBJECT FOR QUERY: xSinImponiblexEmpresa =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xSinImponiblexEmpresa_q]
AS
SELECT Imponible.CI, Imponible.CodEmpresa
FROM Imponible
GROUP BY Imponible.CI, Imponible.CodEmpresa
HAVING (((Sum(Imponible.Importe))=0));
GO

-- ===== DATA OBJECT FOR QUERY: xw_Suma_ValorJornal1 =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xw_Suma_ValorJornal1_q]
AS
SELECT SubsidioCabezalEmpresa.IdSubsidio, Sum(SubsidioCabezalEmpresa.ValorJornal) AS SumaDeValorJornal
FROM SubsidioCabezalEmpresa
GROUP BY SubsidioCabezalEmpresa.IdSubsidio;
GO

-- ===== DATA OBJECT FOR QUERY: zGrupoSubsidio =====
CREATE OR ALTER VIEW [dbo].[acc_sgpa_zGrupoSubsidio_q]
AS
SELECT Anio, (CASE WHEN ValorJornal*30<=5000 THEN '0:5' WHEN ValorJornal*30>5000 And ValorJornal*30<=10000 THEN '5:10' WHEN ValorJornal*30>10000 And ValorJornal*30<=20000 THEN '10:20' WHEN ValorJornal*30>20000 And ValorJornal*30<=30000 THEN '20:30' WHEN ValorJornal*30>30000 And ValorJornal*30<=40000 THEN '30:40' WHEN ValorJornal*30>40000 And ValorJornal*30<=50000 THEN '40:50' WHEN ValorJornal*30>50000 And ValorJornal*30<=60000 THEN '50:60' WHEN ValorJornal*30>60000 THEN '60:' ELSE NULL END) AS Sueldo, Dias
FROM SubsidioCabezal
WHERE Liquidar= 0;
GO

-- ===== DATA OBJECT FOR QUERY: 200_Imponible_6_Meses =====
-- DependsOn: 200_Imponible
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_200_Imponible_6_Meses_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT q_200_Imponible.CI, TRY_CONVERT(int,Sum(q_200_Imponible.Importe)/6) AS Importe
FROM [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2))) Between @pMesIni And @pMes) AND ((q_200_Imponible.Concepto)='1'))
GROUP BY q_200_Imponible.CI
HAVING (((TRY_CONVERT(int,Sum(q_200_Imponible.[Importe])/6))>=(1.25*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 200_Imponible_Ult_Mes =====
-- DependsOn: 200_Imponible
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_200_Imponible_Ult_Mes_q](@pMes INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT q_200_Imponible.CI, TRY_CONVERT(int,Sum(q_200_Imponible.Importe)) AS Importe
FROM [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2)))=@pMes) AND ((q_200_Imponible.Concepto)='1'))
GROUP BY q_200_Imponible.CI
HAVING (((TRY_CONVERT(int,Sum(q_200_Imponible.[Importe])))>=(1.25*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 220_AfiliadoPromedio =====
-- DependsOn: 220_AfiliadoImponibleMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_220_AfiliadoPromedio_q](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
SELECT Avg([220_AfiliadoImponibleMes].Importe) AS Promedio
FROM [acc_sgpa_220_AfiliadoImponibleMes_q](@pCI, @pMes, @pMesIni) AS [220_AfiliadoImponibleMes]
)
GO

-- ===== DATA OBJECT FOR QUERY: 250_Control_Aporte =====
-- DependsOn: 250_ActivosXEmpresaAUnaFecha, 250_AportantesAUnMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_250_Control_Aporte_q](@pFecha DATETIME2(0), @pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT [250_ActivosXEmpresaAUnaFecha].Nombre, [250_ActivosXEmpresaAUnaFecha].Cantidad AS Activos, [250_AportantesAUnMes].Cantidad AS Aportantes
FROM [acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha) AS [250_ActivosXEmpresaAUnaFecha] LEFT JOIN [acc_sgpa_250_AportantesAUnMes_q](@pAnioMes) AS [250_AportantesAUnMes] ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Subsidio =====
-- DependsOn: 300_Rpt_Subsidio
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Subsidio_q]
AS
SELECT *
FROM [acc_sgpa_300_Rpt_Subsidio_q]
WHERE [Mes] = 7 And [Anio] = 2007 And [CI] = 41856014 And Liquidar = 1;
GO

-- ===== DATA OBJECT FOR QUERY: 300_AfiliadoDiasImporte =====
-- DependsOn: 300_TrabajaActivo, 300_AfiliadoAporteOk
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoDiasImporte_q](@pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
SELECT Imponible.CI, Sum(Imponible.DiasTrabajados) AS Dias, Imponible.CodEmpresa, Sum(Imponible.Importe) AS Importe, Sum(Imponible.Importe) / (((@pMesFin / 100 - @pMesIni / 100) * 12 + (@pMesFin % 100 - @pMesIni % 100) + 1) * 30.0) AS Promedio
FROM ((Imponible INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo] ON (Imponible.CI = [300_TrabajaActivo].CI) AND (Imponible.CodEmpresa = [300_TrabajaActivo].CodEmpresa) AND (Imponible.Fechaingreso = [300_TrabajaActivo].FechaIngreso)) INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk] ON ([300_TrabajaActivo].FechaIngreso = [300_AfiliadoAporteOk].Fechaingreso) AND ([300_TrabajaActivo].CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa) AND ([300_TrabajaActivo].CI = [300_AfiliadoAporteOk].CI) AND (Empresa.CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa)
WHERE (((Imponible.Concepto)='1') AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMesFin) AND ((Imponible.CI)=@pCI) AND ((Empresa.Liquidar)=@pLiquidar) AND (([300_AfiliadoAporteOk].Cantidad)>=@pDias))
GROUP BY Imponible.CI, Imponible.CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 400_Promedio_Mes =====
-- DependsOn: 400_Suma_Importe
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_q]
AS
SELECT acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio, Avg(acc_sgpa_400_Suma_Importe_q.Importe) AS Importe
FROM [acc_sgpa_400_Suma_Importe_q]
GROUP BY acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 400_Promedio_Mes_Puesto =====
-- DependsOn: 400_Suma_Puestos
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_Puesto_q]
AS
SELECT acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio, Avg(acc_sgpa_400_Suma_Puestos_q.Importe) AS Importe
FROM [acc_sgpa_400_Suma_Puestos_q]
GROUP BY acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio;
GO

-- ===== DATA OBJECT FOR QUERY: 460_AfiliadoPromedioxCI =====
-- DependsOn: 460_IMS_Actual, 460_Imponible
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_460_AfiliadoPromedioxCI_q](@pCI INT, @pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT acc_sgpa_460_Imponible_q.CI, Avg((acc_sgpa_460_Imponible_q.[Importe]*[460_IMS_Actual].[Importe])/[IMS].[Importe]) AS Promedio
FROM [acc_sgpa_460_IMS_Actual_q](@pAnioMes) AS [460_IMS_Actual], IMS INNER JOIN [acc_sgpa_460_Imponible_q] ON (IMS.Anio = acc_sgpa_460_Imponible_q.Anio) AND (IMS.Mes = acc_sgpa_460_Imponible_q.Mes)
GROUP BY acc_sgpa_460_Imponible_q.CI
HAVING (((acc_sgpa_460_Imponible_q.CI)=@pCI))
)
GO

-- ===== DATA OBJECT FOR QUERY: 480_Rpt_Ficha_Certificacion =====
-- DependsOn: 480_F_Ult_Certif
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Rpt_Ficha_Certificacion_q]
AS
SELECT [600_Afiliado_Certificado].CI, [600_Afiliado_Certificado].Nombres, [600_Afiliado_Certificado].Apellido1, [600_Afiliado_Certificado].Apellido2, [600_Afiliado_Certificado].FechaNacimiento, [600_Afiliado_Certificado].Sexo, [600_Afiliado_Certificado].CodMutualista, [600_Afiliado_Certificado].DescMutualista, [600_Afiliado_Certificado].Especialidad, [600_Afiliado_Certificado].Promedio, [600_Afiliado_Certificado].Empleos, [600_Afiliado_Certificado].DiaProrroga, DATEADD(day,[600_Afiliado_Certificado].DiasUltPro,[600_Afiliado_Certificado].F_Ult_Prorroga) AS F_Ult_Prorroga, [600_Afiliado_Certificado_Afeccion].CodAfeccionTipo, [600_Afiliado_Certificado_Afeccion].DescAfeccionTipo, [600_Afiliado_Certificado_Afeccion].Cantidad, [600_Afiliado_Certificado_Afeccion].Dias, acc_sgpa_480_F_Ult_Certif_q.F_Ult_Certificacion
FROM ([600_Afiliado_Certificado] INNER JOIN [acc_sgpa_480_F_Ult_Certif_q] ON [600_Afiliado_Certificado].CI = acc_sgpa_480_F_Ult_Certif_q.CI) INNER JOIN [600_Afiliado_Certificado_Afeccion] ON [600_Afiliado_Certificado].CI = [600_Afiliado_Certificado_Afeccion].CI;
GO

-- ===== DATA OBJECT FOR QUERY: 480_Prorrogas =====
-- DependsOn: 480_SumaProrrogas
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Prorrogas_q]
AS
SELECT CP.CI, CP.Fecha AS F_Ult_Prorroga, acc_sgpa_480_SumaProrrogas_q.Dias, CP.Dias AS DiasUltPro
FROM CertificacionProrroga AS CP INNER JOIN [acc_sgpa_480_SumaProrrogas_q] ON CP.CI = acc_sgpa_480_SumaProrrogas_q.CI
WHERE ((EXISTS(SELECT 1 FROM CertificacionProrroga AS CP2
WHERE CP.CI = CP2.CI
GROUP BY CP2.CI HAVING MAX(CP2.Fecha) = CP.Fecha)));
GO

-- ===== DATA OBJECT FOR QUERY: 490_SubsidioImporte =====
-- DependsOn: 490_Subsidio
CREATE OR ALTER VIEW [dbo].[acc_sgpa_490_SubsidioImporte_q]
AS
SELECT Afiliado.CI AS CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' ELSE '' END) + [Afiliado].[Apellido2] AS DescAfiliado, acc_sgpa_490_Subsidio_q.ImpLiquido AS Importe
FROM Afiliado INNER JOIN [acc_sgpa_490_Subsidio_q] ON Afiliado.CI = acc_sgpa_490_Subsidio_q.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Cargado_HL =====
-- DependsOn: 500_Rpt_Cargado_HL_Det
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Cargado_HL_q]
AS
SELECT *
FROM [acc_sgpa_500_Rpt_Cargado_HL_Det_q]
WHERE Mes = 07 AND Anio = 2013 AND CodEmpresa = 900;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_DiasCertificacion =====
-- DependsOn: 500_Prorrogas, 500_Rpt_Certificacion_UltFecha
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_q]
AS
SELECT Certificacion.CI, AfeccionTipo.CodAfeccionTipo, MIN(AfeccionTipo.Descrip) AS DescAfeccionTipo, MIN(Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END)) AS DescAfiliado, Sum(DATEDIFF(day,Certificacion.[FechaIni],Certificacion.[FechaFin])+1) AS Dias, Count(*) AS Cantidad, MIN(acc_sgpa_500_Prorrogas_q.Dias) AS Prorrogas, MAX(acc_sgpa_500_Rpt_Certificacion_UltFecha_q.FechaFin) AS F_Ult_Certif, MAX(acc_sgpa_500_Prorrogas_q.Fecha) AS F_Ult_Pro
FROM (((((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo) LEFT JOIN [acc_sgpa_500_Prorrogas_q] ON Certificacion.CI = acc_sgpa_500_Prorrogas_q.CI) INNER JOIN [acc_sgpa_500_Rpt_Certificacion_UltFecha_q] ON Afiliado.CI = acc_sgpa_500_Rpt_Certificacion_UltFecha_q.CI
WHERE ( Certificacion.[CI] = 11391501 )
GROUP BY Certificacion.CI, AfeccionTipo.CodAfeccionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_DiasCertificacion_S =====
-- DependsOn: 500_Prorrogas, 500_Rpt_Certificacion_UltFecha
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_S_q]
AS
SELECT Certificacion.CI, AfeccionTipo.CodAfeccionTipo, MIN(AfeccionTipo.Descrip) AS DescAfeccionTipo, MIN(Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END)) AS DescAfiliado, Sum(DATEDIFF(day,Certificacion.[FechaIni],Certificacion.[FechaFin])+1) AS Dias, Count(*) AS Cantidad, MIN(acc_sgpa_500_Prorrogas_q.Dias) AS Prorrogas, MAX(acc_sgpa_500_Rpt_Certificacion_UltFecha_q.FechaFin) AS F_Ult_Certif, MAX(acc_sgpa_500_Prorrogas_q.Fecha) AS F_Ult_Pro
FROM (((((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo) LEFT JOIN [acc_sgpa_500_Prorrogas_q] ON Certificacion.CI = acc_sgpa_500_Prorrogas_q.CI) INNER JOIN [acc_sgpa_500_Rpt_Certificacion_UltFecha_q] ON Afiliado.CI = acc_sgpa_500_Rpt_Certificacion_UltFecha_q.CI
GROUP BY Certificacion.CI, AfeccionTipo.CodAfeccionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_DetalleSubsidio_Tmp =====
-- DependsOn: 500_Rpt_DetalleSubsidio
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio_Tmp_q]
AS
SELECT (CASE WHEN LEN(CONVERT(nvarchar(max),[D].[CI]))>7 THEN FORMAT([D].[CI],'@\.@@@\.@@@-@') ELSE FORMAT([D].[CI],'@@@\.@@@-@') END) AS CIFormat, D.CI, D.DescAfiliado, D.Mes, D.Anio, D.CodEmpresa, D.DescEmpresa, D.Dias, D.Importe, D.MesCabezal, D.AnioCabezal, D.DiasSubsidio, D.IdSubsidio
FROM [acc_sgpa_500_Rpt_DetalleSubsidio_q] AS D
WHERE [MesCabezal] = 08 And [AnioCabezal] = 2013 And Liquidar = 1;
GO

-- ===== DATA OBJECT FOR QUERY: 506_Export_SubsidioConBPS =====
-- DependsOn: 506_Rpt_Subsidio, 506_Rpt_LiquidacionBPS
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_506_Export_SubsidioConBPS_q](@pMes INT, @pAnio INT)
RETURNS TABLE
AS
RETURN
(
SELECT acc_sgpa_506_Rpt_Subsidio_q.CI, acc_sgpa_506_Rpt_Subsidio_q.Dias, acc_sgpa_506_Rpt_Subsidio_q.Nombres, acc_sgpa_506_Rpt_Subsidio_q.Apellido1, acc_sgpa_506_Rpt_Subsidio_q.Apellido2, acc_sgpa_506_Rpt_Subsidio_q.FechaNacimiento, acc_sgpa_506_Rpt_Subsidio_q.IdSubsidio, acc_sgpa_506_Rpt_Subsidio_q.NroRecibo, acc_sgpa_506_Rpt_Subsidio_q.FechaIni, acc_sgpa_506_Rpt_Subsidio_q.FechaFin, acc_sgpa_506_Rpt_Subsidio_q.FechaIniSubsidio, acc_sgpa_506_Rpt_Subsidio_q.FechaFinSubsidio, acc_sgpa_506_Rpt_Subsidio_q.ImpNominal, acc_sgpa_506_Rpt_Subsidio_q.ImpAguinaldo, acc_sgpa_506_Rpt_Subsidio_q.ImpLiquido, acc_sgpa_506_Rpt_Subsidio_q.Jornal70, acc_sgpa_506_Rpt_Subsidio_q.Aguinaldo70, acc_sgpa_506_Rpt_Subsidio_q.DiasBPS, acc_sgpa_506_Rpt_Subsidio_q.LiquidoBPS, acc_sgpa_506_Rpt_Subsidio_q.LiquidoPagar, acc_sgpa_506_Rpt_Subsidio_q.Banco, acc_sgpa_506_Rpt_Subsidio_q.NroCuenta, [506_Rpt_LiquidacionBPS].MONTO_TOTAL, [506_Rpt_LiquidacionBPS].LIQUIDO, [506_Rpt_LiquidacionBPS].MES_DE_CARGO, [506_Rpt_LiquidacionBPS].NOM_EMPRESA, [506_Rpt_LiquidacionBPS].PCT_POR_EMPRESA, [506_Rpt_LiquidacionBPS].FECHA_PER_DESDE, [506_Rpt_LiquidacionBPS].FECHA_PER_HASTA, [506_Rpt_LiquidacionBPS].[N_ ENTREGA], [506_Rpt_LiquidacionBPS].FECHA_DE_ENTREGA, acc_sgpa_506_Rpt_Subsidio_q.EMail
FROM [acc_sgpa_506_Rpt_Subsidio_q] LEFT JOIN [acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes, @pAnio) AS [506_Rpt_LiquidacionBPS] ON acc_sgpa_506_Rpt_Subsidio_q.CI = [506_Rpt_LiquidacionBPS].CI
WHERE (((acc_sgpa_506_Rpt_Subsidio_q.Mes)=@pMes) AND ((acc_sgpa_506_Rpt_Subsidio_q.Anio)=@pAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 756_NoBaja =====
-- DependsOn: 765_CertificacionContinua, 765_CertificacionEmpalma
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_756_NoBaja_q](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT CI FROM [acc_sgpa_765_CertificacionContinua_q](@pFecha)
UNION SELECT CI FROM [acc_sgpa_765_CertificacionEmpalma_q](@pFecha)
)
GO

-- ===== DATA OBJECT FOR QUERY: 815_AfiliadoImponible =====
-- DependsOn: 800_AfiliadoImponible_Mes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_815_AfiliadoImponible_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Importe
FROM [acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_CI_Todos =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_CI_Todos_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI AS Cantidad
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes
GROUP BY I.CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_Promedio_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_Promedio_Ult6_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg(I.Importe) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes
GROUP BY I.CI
HAVING (Avg([I].[Importe])=0)
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_Promedio_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_Promedio_UltMes_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, TRY_CONVERT(float,Sum(I.Importe)) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))=0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>0_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__0_Ult6_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>0_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__0_UltMes_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, TRY_CONVERT(float,Sum([I].[Importe])) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>=(1.25*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>=(1.25*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 803_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_803_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>=(20*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 803_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_803_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>=(20*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>0_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__0_Ult6_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>0_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__0_UltMes_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))  =  @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>=(20*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) = @pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>=(20*@pSMN)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 811_Afiliado<125_Pct_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25 * @pSMN * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 10)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 20)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 30)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 40)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 50)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 60)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 70)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 80)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 90)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
GROUP BY I.CI
HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*@pSMN))
)
GO

-- ===== DATA OBJECT FOR QUERY: 811_Afiliado<125_Pct_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25 * @pSMN * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 10)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 20)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 30)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 40)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 50)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 60)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 70)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 80)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 90)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*@pSMN))
)
GO

-- ===== DATA OBJECT FOR QUERY: 814_AfiliadoImponibleFranja =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_814_AfiliadoImponibleFranja_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT (CASE WHEN Avg([I].[Importe])<=10000 THEN 'Mas de 0 hasta 10.000' WHEN Avg([I].[Importe])>10000 And Avg([I].[Importe])<=20000 THEN 'Mas de 10.000 hasta 20.000' WHEN Avg([I].[Importe])>20000 THEN 'Mas de 20.000' ELSE NULL END) AS Grupo, I.CI, Avg([I].[Importe]) AS Importe
FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
GROUP BY I.CI
HAVING (((Avg([I].[Importe]))>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 813_CertificadosAfeccion =====
-- DependsOn: 813_CertificacionAfeccionDistintas
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_813_CertificadosAfeccion_q](@pCodPatologia NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT [813_CertificacionAfeccionDistintas].CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descrip, Count(*) AS Cantidad
FROM ((Afiliado INNER JOIN [acc_sgpa_813_CertificacionAfeccionDistintas_q](@pFechaIni, @pFechaFin) AS [813_CertificacionAfeccionDistintas] ON Afiliado.CI = [813_CertificacionAfeccionDistintas].CI) INNER JOIN AfeccionTipo ON [813_CertificacionAfeccionDistintas].CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
WHERE (((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
GROUP BY [813_CertificacionAfeccionDistintas].CodAfeccionTipo
)
GO

-- ===== DATA OBJECT FOR QUERY: 806_CertificadosCantidad =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_806_CertificadosCantidad_q](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
SELECT GE.Descrip AS GE, Count(*) AS Cantidad
FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
WHERE (((A.CI) In (Select CI From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)))
GROUP BY GE.Descrip
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosActivos_GE =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosActivos_GE_q](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT A.CI, GE.Descrip AS DescGrupoEtario
FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
WHERE (((A.CI) In (Select CI From Trabaja Where 
FechaIngreso <= (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END))
)) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 810_AfiliadosActivos_GE2 =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_810_AfiliadosActivos_GE2_q](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT A.CI, A.Edad, GrupoEtario.Descrip AS DescGrupoEtario
FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario
WHERE (((A.CI) In (Select CI From Trabaja Where 
FechaIngreso <= (CASE WHEN @pFecha IS NULL THEN FechaIngreso ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN DATEADD(day,-1,FechaBaja) ELSE FechaBaja END))
)) AND ((A.Edad) Between [GrupoEtario].[EdadIni] And (CASE WHEN [GrupoEtario].[EdadFin]=0 THEN [A].[Edad] ELSE [GrupoEtario].[EdadIni] END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 827_AfiliadosActivos_GE_Sexo =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_827_AfiliadosActivos_GE_Sexo_q](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT A.CI, GE.Descrip AS DescGrupoEtario, A.Sexo
FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
WHERE (((A.CI) In (Select CI From Trabaja Where 
FechaIngCasemed <= (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END))
)) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)))
)
GO

-- ===== DATA OBJECT FOR QUERY: 830_Rpt_Cantidad_Por_Puesto =====
-- DependsOn: 830_CantidadPorPuesto, 830_CantidadPorPuestoNo0
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_830_Rpt_Cantidad_Por_Puesto_q](@pFecha DATETIME2(0), @pMes INT)
RETURNS TABLE
AS
RETURN
(
SELECT Empresa.CodEmpresa, Empresa.Nombre, [830_CantidadPorPuesto].Cantidad, [830_CantidadPorPuestoNo0].Cantidad AS CantidadNo0
FROM (Empresa LEFT JOIN [acc_sgpa_830_CantidadPorPuesto_q](@pFecha) AS [830_CantidadPorPuesto] ON Empresa.CodEmpresa = [830_CantidadPorPuesto].CodEmpresa) LEFT JOIN [acc_sgpa_830_CantidadPorPuestoNo0_q](@pFecha, @pMes) AS [830_CantidadPorPuestoNo0] ON Empresa.CodEmpresa = [830_CantidadPorPuestoNo0].CodEmpresa
WHERE (((Empresa.Ficticia)= 0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Afiliado_Tmp =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Afiliado_Tmp_q]
AS
SELECT (CASE WHEN LEN(acc_sgpa_Rpt_Afiliado_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento, acc_sgpa_Rpt_Afiliado_q.Movil
FROM [acc_sgpa_Rpt_Afiliado_q]
WHERE ( acc_sgpa_Rpt_Afiliado_q.CI IN (SELECT CI FROM Trabaja Where FechaBaja Between TRY_CONVERT(datetime2,'01/05/2014') And TRY_CONVERT(datetime2,'31/05/2014') And CodBajaMotivo IN (5,12,22,3)) );
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_AfiliadoFormatCI =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_AfiliadoFormatCI_q]
AS
SELECT (CASE WHEN LEN(acc_sgpa_Rpt_Afiliado_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento, acc_sgpa_Rpt_Afiliado_q.Movil
FROM [acc_sgpa_Rpt_Afiliado_q] AS acc_sgpa_Rpt_Afiliado_q;
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_AfiliadoNOFormatCI =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_AfiliadoNOFormatCI_q]
AS
SELECT CONVERT(nvarchar(max),acc_sgpa_Rpt_Afiliado_q.CI) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento
FROM [acc_sgpa_Rpt_Afiliado_q] AS acc_sgpa_Rpt_Afiliado_q;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Certificado_Tmp =====
-- DependsOn: Rpt_Certificacion
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Certificado_Tmp_q]
AS
SELECT acc_sgpa_Rpt_Certificacion_q.NroLlamado, (CASE WHEN LEN(acc_sgpa_Rpt_Certificacion_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Certificacion_q.NroRecibo, acc_sgpa_Rpt_Certificacion_q.FechaRecibido, acc_sgpa_Rpt_Certificacion_q.FechaCertificacion, acc_sgpa_Rpt_Certificacion_q.FechaIni, acc_sgpa_Rpt_Certificacion_q.FechaFin, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.CodCertificador, acc_sgpa_Rpt_Certificacion_q.DescCertificador, acc_sgpa_Rpt_Certificacion_q.CodSalidaTipo, acc_sgpa_Rpt_Certificacion_q.DescSalidaTipo, acc_sgpa_Rpt_Certificacion_q.Efectiva, acc_sgpa_Rpt_Certificacion_q.Indicaciones, acc_sgpa_Rpt_Certificacion_q.ImporteDeducible, acc_sgpa_Rpt_Certificacion_q.Usr, acc_sgpa_Rpt_Certificacion_q.Ts, acc_sgpa_Rpt_Certificacion_q.Nombres, acc_sgpa_Rpt_Certificacion_q.Apellido1, acc_sgpa_Rpt_Certificacion_q.Apellido2, acc_sgpa_Rpt_Certificacion_q.DescAfiliado
FROM [acc_sgpa_Rpt_Certificacion_q]
WHERE [NroLlamado] = 4240;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_DiasCertificacion_Tmp =====
-- DependsOn: Rpt_Certificacion
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_Tmp_q]
AS
SELECT acc_sgpa_Rpt_Certificacion_q.CI, (CASE WHEN LEN(acc_sgpa_Rpt_Certificacion_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@@@\.@@@-@') END) AS CIFormat, MIN(acc_sgpa_Rpt_Certificacion_q.DescAfiliado) AS DescAfiliado, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, MIN(acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo) AS DescAfeccionTipo, Sum(DATEDIFF(day,[FechaIni],[FechaFin])+1) AS Dias, Count(*) AS Cantidad
FROM [acc_sgpa_Rpt_Certificacion_q]
GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo;
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_Mutualista_Tmp =====
-- DependsOn: Rpt_Mutualista
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_Mutualista_Tmp_q]
AS
SELECT *
FROM [acc_sgpa_Rpt_Mutualista_q];
GO

-- ===== DATA OBJECT FOR QUERY: 250_AfiliadoConDerecho =====
-- DependsOn: rs_AfiliadoImponibleMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
select ai.ci FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q] as ai
where ai.aniomes between @pMesIni and @pMesFin
group by ai.ci
having (sum(ai.importe)/6) >= (1.25 * @pSMN)
UNION select ai.ci FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q] as ai
where ai.aniomes = @pMesFin
and ai.importe >= (1.25 * @pSMN)
group by ai.ci
)
GO

-- ===== DATA OBJECT FOR QUERY: 480_Promedio =====
-- DependsOn: Rs_AfiliadoImponibleMes
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Promedio_q]
AS
SELECT acc_sgpa_Rs_AfiliadoImponibleMes_q.CI, Avg(acc_sgpa_Rs_AfiliadoImponibleMes_q.Importe) AS Promedio
FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q]
WHERE (((acc_sgpa_Rs_AfiliadoImponibleMes_q.AnioMes) Between (YEAR(DATEADD(month,-7,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-7,CAST(GETDATE() AS date)))) And (YEAR(DATEADD(month,-2,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-2,CAST(GETDATE() AS date))))))
GROUP BY acc_sgpa_Rs_AfiliadoImponibleMes_q.CI;
GO

-- ===== DATA OBJECT FOR QUERY: 320_AfiliadoPromedio =====
-- DependsOn: Rs_AfiliadoImponibleMesNoBaja
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_320_AfiliadoPromedio_q](@pCI INT, @pMesAnioIni INT, @pMesAnioFin INT)
RETURNS TABLE
AS
RETURN
(
SELECT Avg(acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.Importe) AS Importe
FROM [acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]
WHERE (((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.CI)=@pCI) AND ((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.AnioMes) Between @pMesAnioIni And @pMesAnioFin))
)
GO

-- ===== DATA OBJECT FOR QUERY: 320_AfiliadoUltMes =====
-- DependsOn: Rs_AfiliadoImponibleMesNoBaja
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_320_AfiliadoUltMes_q](@pCI INT, @pMesAnio INT)
RETURNS TABLE
AS
RETURN
(
SELECT Avg(acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.Importe) AS Importe
FROM [acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]
WHERE (((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.CI)=@pCI) AND ((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.AnioMes)=@pMesAnio))
)
GO

-- ===== DATA OBJECT FOR QUERY: 100_Create_Bps4Tmp =====
-- DependsOn: Rs_Bps4
CREATE OR ALTER VIEW [dbo].[acc_sgpa_100_Create_Bps4Tmp_q]
AS
SELECT TRY_CONVERT(float,Rs_Bps4.Cedula) AS CI, Rs_Bps4.Concepto, Rs_Bps4.Imponible
FROM [acc_sgpa_Rs_Bps4_q] AS Rs_Bps4;
GO

-- ===== DATA OBJECT FOR QUERY: 480_Certificacion =====
-- DependsOn: Rs_Certificacion_Nombre
CREATE OR ALTER VIEW [dbo].[acc_sgpa_480_Certificacion_q]
AS
SELECT *
FROM [acc_sgpa_Rs_Certificacion_Nombre_q]
WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 );
GO

-- ===== DATA OBJECT FOR QUERY: 510_Rpt_Trabaja =====
-- DependsOn: Rs_Imponible_Ult
CREATE OR ALTER VIEW [dbo].[acc_sgpa_510_Rpt_Trabaja_q]
AS
SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, acc_sgpa_Rs_Imponible_Ult_q.Importe
FROM ((Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN [acc_sgpa_Rs_Imponible_Ult_q] ON (Trabaja.CI = acc_sgpa_Rs_Imponible_Ult_q.CI) AND (Trabaja.CodEmpresa = acc_sgpa_Rs_Imponible_Ult_q.CodEmpresa)
WHERE ( [Trabaja].[CodEmpresa] = 1 );
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_Trabaja_DBG =====
-- DependsOn: Rs_Imponible_Ult
CREATE OR ALTER VIEW [dbo].[acc_sgpa_Rpt_Trabaja_DBG_q]
AS
SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, acc_sgpa_Rs_Imponible_Ult_q.Importe
FROM ((Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN [acc_sgpa_Rs_Imponible_Ult_q] ON (Trabaja.CI = acc_sgpa_Rs_Imponible_Ult_q.CI) AND (Trabaja.CodEmpresa = acc_sgpa_Rs_Imponible_Ult_q.CodEmpresa)
WHERE (((Trabaja.FechaBaja) Is Null));
GO

-- ===== DATA OBJECT FOR QUERY: 150_5_Mejores_Pagos =====
-- DependsOn: Rs_MaxImp_Afiliado
CREATE OR ALTER VIEW [dbo].[acc_sgpa_150_5_Mejores_Pagos_q]
AS
SELECT TOP 5 Afiliado.CI, I.Mes, I.Anio, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres, Empresa.Nombre, I.Importe
FROM Empresa INNER JOIN ((SELECT Imponible.CI, MIN(Imponible.CodEmpresa) AS CodEmpresa, MIN(Imponible.Mes) AS Mes, MIN(Imponible.Anio) AS Anio, Max(Imponible.Importe) AS Importe FROM Imponible GROUP BY Imponible.CI) AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) ON Empresa.CodEmpresa = I.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: acc_sgpa_300_SubsidioItemCod_Full_Data_q =====
-- DependsOn: Rs_SubsidioItemCodXCI
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_acc_sgpa_300_SubsidioItemCod_Full_Data_q_q](@pFecha DATETIME2(0), @pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod.FechaBaja, SubsidioItemCod.ModificaNominal, 0 As CI
FROM SubsidioItemCod
WHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null)) 
UNION ALL SELECT acc_sgpa_Rs_SubsidioItemCodXCI_q.*
FROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]
WHERE (((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaVigencia)<=@pFecha) AND ((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja)>@pFecha Or (acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja) Is Null)) and CI = @pCI
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_CertificadoEmpresa_S =====
-- DependsOn: Rpt_Certificacion, Rs_TrabajaActivo
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa_S_q]
AS
SELECT acc_sgpa_Rpt_Certificacion_q.NroLlamado, acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.NroRecibo, acc_sgpa_Rpt_Certificacion_q.FechaRecibido, acc_sgpa_Rpt_Certificacion_q.FechaCertificacion, acc_sgpa_Rpt_Certificacion_q.FechaIni, acc_sgpa_Rpt_Certificacion_q.FechaFin, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.CodCertificador, acc_sgpa_Rpt_Certificacion_q.DescCertificador, acc_sgpa_Rpt_Certificacion_q.CodSalidaTipo, acc_sgpa_Rpt_Certificacion_q.DescSalidaTipo, acc_sgpa_Rpt_Certificacion_q.Efectiva, acc_sgpa_Rpt_Certificacion_q.Indicaciones, acc_sgpa_Rpt_Certificacion_q.ImporteDeducible, acc_sgpa_Rpt_Certificacion_q.Usr, acc_sgpa_Rpt_Certificacion_q.Ts, acc_sgpa_Rpt_Certificacion_q.Nombres, acc_sgpa_Rpt_Certificacion_q.Apellido1, acc_sgpa_Rpt_Certificacion_q.Apellido2, acc_sgpa_Rpt_Certificacion_q.DescAfiliado, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, Empresa.Nombre AS DescEmpresa
FROM ([acc_sgpa_Rpt_Certificacion_q] AS acc_sgpa_Rpt_Certificacion_q INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON acc_sgpa_Rpt_Certificacion_q.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) INNER JOIN Empresa ON acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: 505_Temporal =====
-- DependsOn: Rs_SubsidioXMes, Rs_TrabajaActivo
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_505_Temporal_q](@pMes NVARCHAR(MAX), @pAno NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT acc_sgpa_Rs_TrabajaActivo_q.IdTrabaja, '1' AS Expr1, 30 AS Expr2, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, acc_sgpa_Rs_SubsidioXMes_q.ImpNominal, acc_sgpa_Rs_SubsidioXMes_q.Anio, acc_sgpa_Rs_SubsidioXMes_q.Mes
FROM [acc_sgpa_Rs_SubsidioXMes_q] INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON acc_sgpa_Rs_SubsidioXMes_q.CI = acc_sgpa_Rs_TrabajaActivo_q.CI
WHERE (((acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa)=900) AND ((acc_sgpa_Rs_SubsidioXMes_q.Anio)=@pAno) AND ((acc_sgpa_Rs_SubsidioXMes_q.Mes)=@pMes))
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_Promedio_Ult6_Todos =====
-- DependsOn: 800_AfiliadoImponible_Mes, Rs_TrabajaActivo
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_Promedio_Ult6_Todos_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT I.CI, Avg(I.Importe) AS Promedio
FROM [acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa) AS I INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON (I.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) AND (I.CI = acc_sgpa_Rs_TrabajaActivo_q.CI)
WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)))
GROUP BY I.CI
HAVING (Avg([I].[Importe])=0)
)
GO

-- ===== DATA OBJECT FOR QUERY: xRptEmpresaCantidad =====
-- DependsOn: xEmpresaCantidad
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xRptEmpresaCantidad_q]
AS
SELECT Empresa.Nombre, acc_sgpa_xEmpresaCantidad_q.Cantidad
FROM [acc_sgpa_xEmpresaCantidad_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaCantidad_q.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xRptPromedioEmpresa =====
-- DependsOn: xEmpresaPromedio
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xRptPromedioEmpresa_q]
AS
SELECT Empresa.Nombre, FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedio_q.PromedioDeImporte), '0.00') AS Promedio
FROM [acc_sgpa_xEmpresaPromedio_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedio_q.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xRptEmpresaPromedioTodo =====
-- DependsOn: xEmpresaPromedioTodo
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xRptEmpresaPromedioTodo_q]
AS
SELECT Empresa.Nombre, FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedioTodo_q.PromedioDeImporte),'0.00') AS Promedio
FROM [acc_sgpa_xEmpresaPromedioTodo_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedioTodo_q.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: xRptMutualistaCantidad =====
-- DependsOn: xMutualistaCantidad
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xRptMutualistaCantidad_q]
AS
SELECT Mutualista.Descrip, acc_sgpa_xMutualistaCantidad_q.Cantidad
FROM [acc_sgpa_xMutualistaCantidad_q] INNER JOIN Mutualista ON acc_sgpa_xMutualistaCantidad_q.CodMutualista = Mutualista.CodMutualista;
GO

-- ===== DATA OBJECT FOR QUERY: xEmpresaPromedioNo0 =====
-- DependsOn: xSinImponiblexEmpresa
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xEmpresaPromedioNo0_q]
AS
SELECT Imponible.CodEmpresa, FORMAT(Avg(Imponible.Importe), '0.00') AS PromedioDeImporte
FROM Imponible LEFT JOIN [acc_sgpa_xSinImponiblexEmpresa_q] ON (Imponible.CodEmpresa = acc_sgpa_xSinImponiblexEmpresa_q.CodEmpresa) AND (Imponible.CI = acc_sgpa_xSinImponiblexEmpresa_q.CI)
WHERE (((TRY_CONVERT(float,[Anio] + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2))) Between 199906 And 199911) AND ((acc_sgpa_xSinImponiblexEmpresa_q.CI) Is Null))
GROUP BY Imponible.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: 200_PagarMutualista =====
-- DependsOn: 200_Imponible_6_Meses, 200_Imponible_Ult_Mes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_200_PagarMutualista_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, SituacionMutual.Pagar
FROM (Afiliado INNER JOIN [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN) AS I ON Afiliado.CI = I.CI) INNER JOIN SituacionMutual ON Afiliado.CodSituacionMutual = SituacionMutual.CodSituacionMutual
WHERE (((Afiliado.CodMutualista)>0) AND ((SituacionMutual.Pagar)= 1))
UNION SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, SituacionMutual.Pagar
FROM (Afiliado INNER JOIN [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN) AS I ON Afiliado.CI = I.CI) INNER JOIN SituacionMutual ON Afiliado.CodSituacionMutual = SituacionMutual.CodSituacionMutual
WHERE (((Afiliado.CodMutualista)>0) AND ((SituacionMutual.Pagar)= 1))
)
GO

-- ===== DATA OBJECT FOR QUERY: 201_PagarMutualista =====
-- DependsOn: 200_Imponible_6_Meses, 200_Imponible_Ult_Mes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_201_PagarMutualista_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT *
FROM [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN)
UNION SELECT * FROM [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN)
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_AfiliadoValorJornal =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoValorJornal_q](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
SELECT [300_AfiliadoDiasImporte].CI, Sum([300_AfiliadoDiasImporte].Promedio) AS Promedio
FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)<>@pCodCasemed))
GROUP BY [300_AfiliadoDiasImporte].CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_AfiliadoValorJornalCasemed =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoValorJornalCasemed_q](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
SELECT [300_AfiliadoDiasImporte].CI, Sum(([Importe]/180)) AS Promedio
FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)=@pCodCasemed))
GROUP BY [300_AfiliadoDiasImporte].CI
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_AfiliadoValorJornalxEmpresa =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_AfiliadoValorJornalxEmpresa_q](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
SELECT [300_AfiliadoDiasImporte].CI, [300_AfiliadoDiasImporte].CodEmpresa, Sum([300_AfiliadoDiasImporte].Promedio) AS Promedio
FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)<>@pCodCasemed))
GROUP BY [300_AfiliadoDiasImporte].CI, [300_AfiliadoDiasImporte].CodEmpresa
)
GO

-- ===== DATA OBJECT FOR QUERY: 400_Promedio_Gral =====
-- DependsOn: 400_Promedio_Mes
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Gral_q]
AS
SELECT Avg(acc_sgpa_400_Promedio_Mes_q.Importe) AS PromedioDeImporte
FROM [acc_sgpa_400_Promedio_Mes_q] AS acc_sgpa_400_Promedio_Mes_q;
GO

-- ===== DATA OBJECT FOR QUERY: 400_Promedio_Gral_Puesto =====
-- DependsOn: 400_Promedio_Mes_Puesto
CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Gral_Puesto_q]
AS
SELECT Avg(acc_sgpa_400_Promedio_Mes_Puesto_q.Importe) AS PromedioDeImporte
FROM [acc_sgpa_400_Promedio_Mes_Puesto_q] AS acc_sgpa_400_Promedio_Mes_Puesto_q;
GO

-- ===== DATA OBJECT FOR QUERY: 801_Cantidad_Todos =====
-- DependsOn: 801_CI_Todos
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_Cantidad_Todos_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_801_CI_Todos_q](@pMes, @pMesIni, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_Ult6_Cantidad =====
-- DependsOn: 801_Promedio_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_Ult6_Cantidad_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_801_Promedio_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 801_UltMes_Cantidad =====
-- DependsOn: 801_Promedio_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_801_UltMes_Cantidad_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_801_Promedio_UltMes_q](@pMes, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>0_Cantidad_Ult6 =====
-- DependsOn: 802_>0_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__0_Cantidad_Ult6_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_802__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>0_Cantidad_UltMes =====
-- DependsOn: 802_>0_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__0_Cantidad_UltMes_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_802__0_UltMes_q](@pMes, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>125_Cantidad_Ult6 =====
-- DependsOn: 802_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__125_Cantidad_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_802_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 802_>125_Cantidad_UltMes =====
-- DependsOn: 802_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_802__125_Cantidad_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_802_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 803_>20_Cantidad_Ult6 =====
-- DependsOn: 803_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_803__20_Cantidad_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_803_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 803_>20_Cantidad_UltMes =====
-- DependsOn: 803_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_803__20_Cantidad_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count(*) AS Cantidad
FROM [acc_sgpa_803_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>0_Masa_Ult6 =====
-- DependsOn: 804_>0_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__0_Masa_Ult6_q](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum([804_>0_Ult6].Promedio) AS Masa
FROM [acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa) AS [804_>0_Ult6]
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>0_Masa_UltMes =====
-- DependsOn: 804_>0_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__0_Masa_UltMes_q](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum([804_>0_UltMes].Promedio) AS Masa
FROM [acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa) AS [804_>0_UltMes]
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>20_Masa_Ult6 =====
-- DependsOn: 804_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__20_Masa_Ult6_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum([804_Ult6].Promedio) AS Masa
FROM [acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [804_Ult6]
)
GO

-- ===== DATA OBJECT FOR QUERY: 804_>20_Masa_UltMes =====
-- DependsOn: 804_UltMes
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_804__20_Masa_UltMes_q](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Sum([804_UltMes].Promedio) AS Masa
FROM [acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [804_UltMes]
)
GO

-- ===== DATA OBJECT FOR QUERY: 811_AfiliadoCantidad =====
-- DependsOn: 811_Afiliado<125_Pct_Ult6
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_811_AfiliadoCantidad_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
SELECT Count([811_Afiliado<125_Pct_Ult6].CI) AS CountOfCI
FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6]
)
GO

-- ===== DATA OBJECT FOR QUERY: 250_AfiliadoaControlar =====
-- DependsOn: 250_AfiliadoConDerecho
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_250_AfiliadoaControlar_q](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CI, Trabaja.CodEmpresa
FROM ((([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
WHERE (Trabaja.FechaBaja Is Null) AND Mutualista.Ficticia= 0 AND Afiliado.CodSituacionMutual='AF' AND Empresa.Ficticia = 0
)
GO

-- ===== DATA OBJECT FOR QUERY: zConsulta2 =====
-- DependsOn: 250_AfiliadoConDerecho
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_zConsulta2_q](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
SELECT Trabaja.CI, Trabaja.CodEmpresa
FROM ((([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
WHERE (Trabaja.FechaBaja Is Null) AND Mutualista.Ficticia= 0 AND Afiliado.CodSituacionMutual='AF' AND Empresa.Ficticia = 0
)
GO

-- ===== DATA OBJECT FOR QUERY: 300_SubsidioItemCod_Full =====
-- DependsOn: acc_sgpa_300_SubsidioItemCod_Full_Data_q
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_300_SubsidioItemCod_Full_q](@pFecha DATETIME2(0), @pCI INT)
RETURNS TABLE
AS
RETURN
(
SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod.FechaBaja, SubsidioItemCod.ModificaNominal, 0 As CI
FROM SubsidioItemCod
WHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null))
UNION ALL SELECT acc_sgpa_Rs_SubsidioItemCodXCI_q.*
FROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]
WHERE (((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaVigencia)<=@pFecha) AND ((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja)>@pFecha Or (acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja) Is Null)) and CI = @pCI
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_CertificadoEmpresa =====
-- DependsOn: 500_Rpt_CertificadoEmpresa_S
CREATE OR ALTER VIEW [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa_q]
AS
SELECT acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.NroLlamado, (CASE WHEN LEN(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI])>=8 THEN FORMAT(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI],'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI],'@@@\.@@@-@') END) AS CI, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.NroRecibo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaRecibido, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaCertificacion, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaIni, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaFin, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodAfeccionTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescAfeccionTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodCertificador, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescCertificador, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodSalidaTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescSalidaTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Efectiva, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Indicaciones, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.ImporteDeducible, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Usr, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Ts, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Nombres, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Apellido1, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Apellido2, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescAfiliado, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodEmpresa, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescEmpresa
FROM [acc_sgpa_500_Rpt_CertificadoEmpresa_S_q]
WHERE (acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI] = 13606922);
GO

-- ===== DATA OBJECT FOR QUERY: xRptEmpresaPromedioNo0 =====
-- DependsOn: xEmpresaPromedioNo0
CREATE OR ALTER VIEW [dbo].[acc_sgpa_xRptEmpresaPromedioNo0_q]
AS
SELECT Empresa.Nombre, acc_sgpa_xEmpresaPromedioNo0_q.PromedioDeImporte AS Promedio
FROM [acc_sgpa_xEmpresaPromedioNo0_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedioNo0_q.CodEmpresa = Empresa.CodEmpresa;
GO

-- ===== DATA OBJECT FOR QUERY: zConsulta1 =====
-- DependsOn: 200_PagarMutualista
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_zConsulta1_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT acc_sgpa_200_PagarMutualista_q.CI, Trabaja.CodEmpresa
FROM (([acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS acc_sgpa_200_PagarMutualista_q INNER JOIN Afiliado ON acc_sgpa_200_PagarMutualista_q.CI = Afiliado.CI) INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
WHERE (((Afiliado.CodSituacionMutual)='AF') AND ((Afiliado.CodMutualista) NOT In (0,38,40)) AND ((Trabaja.FechaBaja) Is Null))
)
GO

-- ===== DATA OBJECT FOR QUERY: Rpt_NoPagarMutualista =====
-- DependsOn: 201_PagarMutualista, Rs_TrabajaActivo
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_Rpt_NoPagarMutualista_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, Empresa.Nombre AS DescEmpresa, Mutualista.Descrip AS DescMutualista
FROM (((Afiliado LEFT JOIN [acc_sgpa_201_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS [201_PagarMutualista] ON Afiliado.CI = [201_PagarMutualista].CI) INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON Afiliado.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) INNER JOIN Empresa ON acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
WHERE ((([201_PagarMutualista].CI) Is Null) AND ((Afiliado.CodMutualista)>0))
)
GO

-- ===== DATA OBJECT FOR QUERY: 500_Rpt_NoPagarMutualista =====
-- DependsOn: Rpt_NoPagarMutualista
CREATE OR ALTER FUNCTION [dbo].[acc_sgpa_500_Rpt_NoPagarMutualista_q](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
SELECT (CASE WHEN LEN(acc_sgpa_Rpt_NoPagarMutualista_q.[CI])>=8 THEN FORMAT(acc_sgpa_Rpt_NoPagarMutualista_q.[CI],'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_NoPagarMutualista_q.[CI],'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_NoPagarMutualista_q.Nombres, acc_sgpa_Rpt_NoPagarMutualista_q.Apellido1, acc_sgpa_Rpt_NoPagarMutualista_q.Apellido2, acc_sgpa_Rpt_NoPagarMutualista_q.FechaNacimiento, acc_sgpa_Rpt_NoPagarMutualista_q.Sexo, acc_sgpa_Rpt_NoPagarMutualista_q.Direccion, acc_sgpa_Rpt_NoPagarMutualista_q.Telefono, acc_sgpa_Rpt_NoPagarMutualista_q.EMail, acc_sgpa_Rpt_NoPagarMutualista_q.CodMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.FechaIngMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.NroSocioMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.CodRegimenJubilatorio, acc_sgpa_Rpt_NoPagarMutualista_q.CodDepartamento, acc_sgpa_Rpt_NoPagarMutualista_q.PagaMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.Usr, acc_sgpa_Rpt_NoPagarMutualista_q.Ts, acc_sgpa_Rpt_NoPagarMutualista_q.DescAfiliado, acc_sgpa_Rpt_NoPagarMutualista_q.CodEmpresa, acc_sgpa_Rpt_NoPagarMutualista_q.DescEmpresa, acc_sgpa_Rpt_NoPagarMutualista_q.DescMutualista
FROM [acc_sgpa_Rpt_NoPagarMutualista_q](@pMes, @pMesIni, @pSMN)
)
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_AfeccionGrupo_Max =====
IF OBJECT_ID('dbo.001_AfeccionGrupo_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_AfeccionGrupo_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_AfeccionGrupo_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_AfeccionTipo_Max =====
IF OBJECT_ID('dbo.001_AfeccionTipo_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_AfeccionTipo_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_AfeccionTipo_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Certificacion_Max =====
IF OBJECT_ID('dbo.001_Certificacion_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Certificacion_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Certificacion_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Certificador_Max =====
IF OBJECT_ID('dbo.001_Certificador_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Certificador_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Certificador_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Empresa_Max =====
IF OBJECT_ID('dbo.001_Empresa_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Empresa_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Empresa_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_FormaPago_Max =====
IF OBJECT_ID('dbo.001_FormaPago_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_FormaPago_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_FormaPago_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Mutualista_Max =====
IF OBJECT_ID('dbo.001_Mutualista_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Mutualista_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Mutualista_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Patologia_Max =====
IF OBJECT_ID('dbo.001_Patologia_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Patologia_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Patologia_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_PrestacionTipo_Max =====
IF OBJECT_ID('dbo.001_PrestacionTipo_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_PrestacionTipo_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_PrestacionTipo_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_Recibo_Max =====
IF OBJECT_ID('dbo.001_Recibo_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_Recibo_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_Recibo_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_RegimenJubilatorio_Max =====
IF OBJECT_ID('dbo.001_RegimenJubilatorio_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_RegimenJubilatorio_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_RegimenJubilatorio_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_SalidaTipo_Max =====
IF OBJECT_ID('dbo.001_SalidaTipo_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_SalidaTipo_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_SalidaTipo_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 001_SituacionPago_Max =====
IF OBJECT_ID('dbo.001_SituacionPago_Max') IS NULL EXEC('CREATE VIEW [dbo].[001_SituacionPago_Max] AS SELECT * FROM [dbo].[acc_sgpa_001_SituacionPago_Max_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 100_Afiliado_CI =====
IF OBJECT_ID('dbo.100_Afiliado_CI') IS NULL EXEC('CREATE FUNCTION [dbo].[100_Afiliado_CI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_100_Afiliado_CI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 100_CargadoHL =====
IF OBJECT_ID('dbo.100_CargadoHL') IS NULL EXEC('CREATE FUNCTION [dbo].[100_CargadoHL](@pCodEmpresa NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pAnio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_100_CargadoHL_q](@pCodEmpresa, @pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 100_CargadosHL =====
IF OBJECT_ID('dbo.100_CargadosHL') IS NULL EXEC('CREATE FUNCTION [dbo].[100_CargadosHL](@pMes TINYINT, @pAnio INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_100_CargadosHL_q](@pMes, @pAnio, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 100_TrabajaActivo =====
IF OBJECT_ID('dbo.100_TrabajaActivo') IS NULL EXEC('CREATE FUNCTION [dbo].[100_TrabajaActivo](@pMes INT, @pAno INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_100_TrabajaActivo_q](@pMes, @pAno)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 101_ImponibleMes =====
IF OBJECT_ID('dbo.101_ImponibleMes') IS NULL EXEC('CREATE FUNCTION [dbo].[101_ImponibleMes](@pMes INT, @pAno INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_101_ImponibleMes_q](@pMes, @pAno)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 102_DiasCertificados =====
IF OBJECT_ID('dbo.102_DiasCertificados') IS NULL EXEC('CREATE FUNCTION [dbo].[102_DiasCertificados](@pCI INT, @pNroLlamado INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_102_DiasCertificados_q](@pCI, @pNroLlamado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 102_Prestacion_Cantidad =====
IF OBJECT_ID('dbo.102_Prestacion_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[102_Prestacion_Cantidad](@pCI INT, @pCodPrestacionTipo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_102_Prestacion_Cantidad_q](@pCI, @pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 103_PrestacionesAfiliado =====
IF OBJECT_ID('dbo.103_PrestacionesAfiliado') IS NULL EXEC('CREATE FUNCTION [dbo].[103_PrestacionesAfiliado](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_103_PrestacionesAfiliado_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 103_ReintegrosAfiliado =====
IF OBJECT_ID('dbo.103_ReintegrosAfiliado') IS NULL EXEC('CREATE FUNCTION [dbo].[103_ReintegrosAfiliado](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_103_ReintegrosAfiliado_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 110_Imponible_Periodo =====
IF OBJECT_ID('dbo.110_Imponible_Periodo') IS NULL EXEC('CREATE FUNCTION [dbo].[110_Imponible_Periodo](@pCI INT, @pMesIni INT, @pMesFin INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_110_Imponible_Periodo_q](@pCI, @pMesIni, @pMesFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 110_PrimaFallecimiento_CI =====
IF OBJECT_ID('dbo.110_PrimaFallecimiento_CI') IS NULL EXEC('CREATE FUNCTION [dbo].[110_PrimaFallecimiento_CI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_110_PrimaFallecimiento_CI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 145_AfiliadoXCI =====
IF OBJECT_ID('dbo.145_AfiliadoXCI') IS NULL EXEC('CREATE FUNCTION [dbo].[145_AfiliadoXCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_145_AfiliadoXCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 150_5_Mejores_Pagos_No_Casmu =====
IF OBJECT_ID('dbo.150_5_Mejores_Pagos_No_Casmu') IS NULL EXEC('CREATE VIEW [dbo].[150_5_Mejores_Pagos_No_Casmu] AS SELECT * FROM [dbo].[acc_sgpa_150_5_Mejores_Pagos_No_Casmu_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 160_Trabaja =====
IF OBJECT_ID('dbo.160_Trabaja') IS NULL EXEC('CREATE FUNCTION [dbo].[160_Trabaja](@pCI INT, @pCodEmpresa INT, @pFechaIngreso DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_160_Trabaja_q](@pCI, @pCodEmpresa, @pFechaIngreso)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 160_Trabaja_CI =====
IF OBJECT_ID('dbo.160_Trabaja_CI') IS NULL EXEC('CREATE FUNCTION [dbo].[160_Trabaja_CI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_160_Trabaja_CI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 180_GrupoEtario_EdadIni =====
IF OBJECT_ID('dbo.180_GrupoEtario_EdadIni') IS NULL EXEC('CREATE FUNCTION [dbo].[180_GrupoEtario_EdadIni](@pEdadIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_180_GrupoEtario_EdadIni_q](@pEdadIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 200_Imponible =====
IF OBJECT_ID('dbo.200_Imponible') IS NULL EXEC('CREATE FUNCTION [dbo].[200_Imponible](@pMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_200_Imponible_q](@pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_ImpRetObrero =====
IF OBJECT_ID('dbo.210_ImpRetObrero') IS NULL EXEC('CREATE FUNCTION [dbo].[210_ImpRetObrero](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_ImpRetObrero_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_ImpRetPatronal =====
IF OBJECT_ID('dbo.210_ImpRetPatronal') IS NULL EXEC('CREATE FUNCTION [dbo].[210_ImpRetPatronal](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_ImpRetPatronal_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_ImpRetTotal =====
IF OBJECT_ID('dbo.210_ImpRetTotal') IS NULL EXEC('CREATE FUNCTION [dbo].[210_ImpRetTotal](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_ImpRetTotal_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_MontoGrabado =====
IF OBJECT_ID('dbo.210_MontoGrabado') IS NULL EXEC('CREATE FUNCTION [dbo].[210_MontoGrabado](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_MontoGrabado_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_SubsidioCantidad =====
IF OBJECT_ID('dbo.210_SubsidioCantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[210_SubsidioCantidad](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_SubsidioCantidad_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_TotImpEmp =====
IF OBJECT_ID('dbo.210_TotImpEmp') IS NULL EXEC('CREATE FUNCTION [dbo].[210_TotImpEmp](@pMes NVARCHAR(MAX), @pAnio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_TotImpEmp_q](@pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 210_TotTributo =====
IF OBJECT_ID('dbo.210_TotTributo') IS NULL EXEC('CREATE FUNCTION [dbo].[210_TotTributo](@pMes NVARCHAR(MAX), @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_210_TotTributo_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 220_AfiliadoImponibleMes =====
IF OBJECT_ID('dbo.220_AfiliadoImponibleMes') IS NULL EXEC('CREATE FUNCTION [dbo].[220_AfiliadoImponibleMes](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_220_AfiliadoImponibleMes_q](@pCI, @pMes, @pMesIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 220_AfiliadoImponibleMes_Old =====
IF OBJECT_ID('dbo.220_AfiliadoImponibleMes_Old') IS NULL EXEC('CREATE FUNCTION [dbo].[220_AfiliadoImponibleMes_Old](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_220_AfiliadoImponibleMes_Old_q](@pCI, @pMes, @pMesIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 230_PrestacionAnterior =====
IF OBJECT_ID('dbo.230_PrestacionAnterior') IS NULL EXEC('CREATE FUNCTION [dbo].[230_PrestacionAnterior](@pCI INT, @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_230_PrestacionAnterior_q](@pCI, @pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 230_PrestacionTipoFromCod =====
IF OBJECT_ID('dbo.230_PrestacionTipoFromCod') IS NULL EXEC('CREATE FUNCTION [dbo].[230_PrestacionTipoFromCod](@pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_230_PrestacionTipoFromCod_q](@pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 230_Receta =====
IF OBJECT_ID('dbo.230_Receta') IS NULL EXEC('CREATE FUNCTION [dbo].[230_Receta](@pCI INT, @pFecha DATETIME2(0), @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_230_Receta_q](@pCI, @pFecha, @pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 230_Receta_Anterior =====
IF OBJECT_ID('dbo.230_Receta_Anterior') IS NULL EXEC('CREATE FUNCTION [dbo].[230_Receta_Anterior](@pCI INT, @pFecha DATETIME2(0), @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_230_Receta_Anterior_q](@pCI, @pFecha, @pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 230_Receta_Ultima =====
IF OBJECT_ID('dbo.230_Receta_Ultima') IS NULL EXEC('CREATE FUNCTION [dbo].[230_Receta_Ultima](@pCI INT, @pCodPrestacionTipo INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_230_Receta_Ultima_q](@pCI, @pCodPrestacionTipo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 240_Grupos_IE =====
IF OBJECT_ID('dbo.240_Grupos_IE') IS NULL EXEC('CREATE VIEW [dbo].[240_Grupos_IE] AS SELECT * FROM [dbo].[acc_sgpa_240_Grupos_IE_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 240_InformeGrupo =====
IF OBJECT_ID('dbo.240_InformeGrupo') IS NULL EXEC('CREATE FUNCTION [dbo].[240_InformeGrupo](@pGrupo NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_240_InformeGrupo_q](@pGrupo)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 240_InformeIdRpt =====
IF OBJECT_ID('dbo.240_InformeIdRpt') IS NULL EXEC('CREATE FUNCTION [dbo].[240_InformeIdRpt](@pIdRpt INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_240_InformeIdRpt_q](@pIdRpt)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 250_ActivosXEmpresaAUnaFecha =====
IF OBJECT_ID('dbo.250_ActivosXEmpresaAUnaFecha') IS NULL EXEC('CREATE FUNCTION [dbo].[250_ActivosXEmpresaAUnaFecha](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 250_AportantesAUnMes =====
IF OBJECT_ID('dbo.250_AportantesAUnMes') IS NULL EXEC('CREATE FUNCTION [dbo].[250_AportantesAUnMes](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_250_AportantesAUnMes_q](@pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_AfiliadoAporteOk =====
IF OBJECT_ID('dbo.300_AfiliadoAporteOk') IS NULL EXEC('CREATE FUNCTION [dbo].[300_AfiliadoAporteOk](@pMesIniImp INT, @pMesFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_CertificacionCIDia =====
IF OBJECT_ID('dbo.300_CertificacionCIDia') IS NULL EXEC('CREATE FUNCTION [dbo].[300_CertificacionCIDia](@pCI INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_CertificacionCIDia_q](@pCI, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_CertificacionesAfiliadoMes =====
IF OBJECT_ID('dbo.300_CertificacionesAfiliadoMes') IS NULL EXEC('CREATE FUNCTION [dbo].[300_CertificacionesAfiliadoMes](@pCI INT, @pMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_CertificacionesAfiliadoMes_q](@pCI, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_CertificacionesTmp =====
IF OBJECT_ID('dbo.300_CertificacionesTmp') IS NULL EXEC('CREATE VIEW [dbo].[300_CertificacionesTmp] AS SELECT * FROM [dbo].[acc_sgpa_300_CertificacionesTmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_EmpresaxIDSubsidio =====
IF OBJECT_ID('dbo.300_EmpresaxIDSubsidio') IS NULL EXEC('CREATE FUNCTION [dbo].[300_EmpresaxIDSubsidio](@pIDSubsidio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_EmpresaxIDSubsidio_q](@pIDSubsidio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_JornalAnterior =====
IF OBJECT_ID('dbo.300_JornalAnterior') IS NULL EXEC('CREATE FUNCTION [dbo].[300_JornalAnterior](@pMes INT, @pCi INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_JornalAnterior_q](@pMes, @pCi, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_JornalAnterior2 =====
IF OBJECT_ID('dbo.300_JornalAnterior2') IS NULL EXEC('CREATE FUNCTION [dbo].[300_JornalAnterior2](@pCI INT, @pLiquidar NVARCHAR(MAX), @pMes INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_JornalAnterior2_q](@pCI, @pLiquidar, @pMes, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_JornalAnterior2Ant =====
IF OBJECT_ID('dbo.300_JornalAnterior2Ant') IS NULL EXEC('CREATE FUNCTION [dbo].[300_JornalAnterior2Ant](@pCI INT, @pLiquidar NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_JornalAnterior2Ant_q](@pCI, @pLiquidar, @pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_RegimenJubilatorioAfiliado =====
IF OBJECT_ID('dbo.300_RegimenJubilatorioAfiliado') IS NULL EXEC('CREATE FUNCTION [dbo].[300_RegimenJubilatorioAfiliado](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_RegimenJubilatorioAfiliado_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_Rpt_PrimaFallecimiento =====
IF OBJECT_ID('dbo.300_Rpt_PrimaFallecimiento') IS NULL EXEC('CREATE VIEW [dbo].[300_Rpt_PrimaFallecimiento] AS SELECT * FROM [dbo].[acc_sgpa_300_Rpt_PrimaFallecimiento_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_Rpt_Subsidio =====
IF OBJECT_ID('dbo.300_Rpt_Subsidio') IS NULL EXEC('CREATE VIEW [dbo].[300_Rpt_Subsidio] AS SELECT * FROM [dbo].[acc_sgpa_300_Rpt_Subsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_Rpt_SubsidioEmpresa =====
IF OBJECT_ID('dbo.300_Rpt_SubsidioEmpresa') IS NULL EXEC('CREATE VIEW [dbo].[300_Rpt_SubsidioEmpresa] AS SELECT * FROM [dbo].[acc_sgpa_300_Rpt_SubsidioEmpresa_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioEmpresaAnterior =====
IF OBJECT_ID('dbo.300_SubsidioEmpresaAnterior') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioEmpresaAnterior](@pIdSubsidio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioEmpresaAnterior_q](@pIdSubsidio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioEmpresaAnteriorVacia =====
IF OBJECT_ID('dbo.300_SubsidioEmpresaAnteriorVacia') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioEmpresaAnteriorVacia](@pIdSubsidio INT, @pLiquidar NVARCHAR(MAX), @pCodCasemed NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVacia_q](@pIdSubsidio, @pLiquidar, @pCodCasemed)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioEmpresaAnteriorVaciaCasemed =====
IF OBJECT_ID('dbo.300_SubsidioEmpresaAnteriorVaciaCasemed') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioEmpresaAnteriorVaciaCasemed](@pIdSubsidio INT, @pLiquidar NVARCHAR(MAX), @pCodCasemed NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVaciaCasemed_q](@pIdSubsidio, @pLiquidar, @pCodCasemed)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioEnfermedadFromMes =====
IF OBJECT_ID('dbo.300_SubsidioEnfermedadFromMes') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioEnfermedadFromMes](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioEnfermedadFromMes_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioFecha =====
IF OBJECT_ID('dbo.300_SubsidioFecha') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioFecha](@pMes INT, @pAnio INT, @pLiquidar NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioFecha_q](@pMes, @pAnio, @pLiquidar)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioFranjaAnt =====
IF OBJECT_ID('dbo.300_SubsidioFranjaAnt') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioFranjaAnt](@pCodSubsidioItemCod INT, @pImporte NVARCHAR(MAX), @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioFranjaAnt_q](@pCodSubsidioItemCod, @pImporte, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioImporte =====
IF OBJECT_ID('dbo.300_SubsidioImporte') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioImporte](@pIdSubsidio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioImporte_q](@pIdSubsidio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioItemCod =====
IF OBJECT_ID('dbo.300_SubsidioItemCod') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioItemCod](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioItemCod_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioItemId =====
IF OBJECT_ID('dbo.300_SubsidioItemId') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioItemId](@pCodSubsidioItemCod INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioItemId_q](@pCodSubsidioItemCod)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_TrabajaActivo =====
IF OBJECT_ID('dbo.300_TrabajaActivo') IS NULL EXEC('CREATE FUNCTION [dbo].[300_TrabajaActivo](@pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_TrabajaActivo_q](@pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 310_CertificacionAnterior =====
IF OBJECT_ID('dbo.310_CertificacionAnterior') IS NULL EXEC('CREATE FUNCTION [dbo].[310_CertificacionAnterior](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCI INT, @pNroLlamado INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_310_CertificacionAnterior_q](@pFechaIni, @pFechaFin, @pCI, @pNroLlamado)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Suma_Importe =====
IF OBJECT_ID('dbo.400_Suma_Importe') IS NULL EXEC('CREATE VIEW [dbo].[400_Suma_Importe] AS SELECT * FROM [dbo].[acc_sgpa_400_Suma_Importe_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Suma_Puestos =====
IF OBJECT_ID('dbo.400_Suma_Puestos') IS NULL EXEC('CREATE VIEW [dbo].[400_Suma_Puestos] AS SELECT * FROM [dbo].[acc_sgpa_400_Suma_Puestos_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 401_TrabajaActivoxCI =====
IF OBJECT_ID('dbo.401_TrabajaActivoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[401_TrabajaActivoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_401_TrabajaActivoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 403_AportesUlt12xCI =====
IF OBJECT_ID('dbo.403_AportesUlt12xCI') IS NULL EXEC('CREATE FUNCTION [dbo].[403_AportesUlt12xCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_403_AportesUlt12xCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 403_UltProxCI =====
IF OBJECT_ID('dbo.403_UltProxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[403_UltProxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_403_UltProxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 450_AfiliadoMutualista =====
IF OBJECT_ID('dbo.450_AfiliadoMutualista') IS NULL EXEC('CREATE FUNCTION [dbo].[450_AfiliadoMutualista](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_450_AfiliadoMutualista_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_AdPreJubxCI =====
IF OBJECT_ID('dbo.460_AdPreJubxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_AdPreJubxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_AdPreJubxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_AfiliadoCertificadoxCI =====
IF OBJECT_ID('dbo.460_AfiliadoCertificadoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_AfiliadoCertificadoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_AfiliadoCertificadoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_AfiliadoSubsidioxCI =====
IF OBJECT_ID('dbo.460_AfiliadoSubsidioxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_AfiliadoSubsidioxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_AfiliadoSubsidioxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_Imponible =====
IF OBJECT_ID('dbo.460_Imponible') IS NULL EXEC('CREATE VIEW [dbo].[460_Imponible] AS SELECT * FROM [dbo].[acc_sgpa_460_Imponible_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_IMSxAnioMes =====
IF OBJECT_ID('dbo.460_IMSxAnioMes') IS NULL EXEC('CREATE FUNCTION [dbo].[460_IMSxAnioMes](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_IMSxAnioMes_q](@pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_IMS_Actual =====
IF OBJECT_ID('dbo.460_IMS_Actual') IS NULL EXEC('CREATE FUNCTION [dbo].[460_IMS_Actual](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_IMS_Actual_q](@pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_RegimenJubilatorioxCod =====
IF OBJECT_ID('dbo.460_RegimenJubilatorioxCod') IS NULL EXEC('CREATE FUNCTION [dbo].[460_RegimenJubilatorioxCod](@pCodRegimenJubilatorio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_RegimenJubilatorioxCod_q](@pCodRegimenJubilatorio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_TrabajaActivoxCI =====
IF OBJECT_ID('dbo.460_TrabajaActivoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_TrabajaActivoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_TrabajaActivoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_UltSMN =====
IF OBJECT_ID('dbo.460_UltSMN') IS NULL EXEC('CREATE FUNCTION [dbo].[460_UltSMN](@pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_UltSMN_q](@pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_UltSubsidioxCI =====
IF OBJECT_ID('dbo.460_UltSubsidioxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_UltSubsidioxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_UltSubsidioxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 470_AdPreJubPagoxCI =====
IF OBJECT_ID('dbo.470_AdPreJubPagoxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[470_AdPreJubPagoxCI](@pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_470_AdPreJubPagoxCI_q](@pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 470_AdPreJubPagoxCI-Mes =====
IF OBJECT_ID('dbo.470_AdPreJubPagoxCI-Mes') IS NULL EXEC('CREATE FUNCTION [dbo].[470_AdPreJubPagoxCI-Mes](@pCI INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_470_AdPreJubPagoxCI_Mes_q](@pCI, @pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_F_Ult_Certif =====
IF OBJECT_ID('dbo.480_F_Ult_Certif') IS NULL EXEC('CREATE VIEW [dbo].[480_F_Ult_Certif] AS SELECT * FROM [dbo].[acc_sgpa_480_F_Ult_Certif_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_SumaProrrogas =====
IF OBJECT_ID('dbo.480_SumaProrrogas') IS NULL EXEC('CREATE VIEW [dbo].[480_SumaProrrogas] AS SELECT * FROM [dbo].[acc_sgpa_480_SumaProrrogas_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 490_Subsidio =====
IF OBJECT_ID('dbo.490_Subsidio') IS NULL EXEC('CREATE VIEW [dbo].[490_Subsidio] AS SELECT * FROM [dbo].[acc_sgpa_490_Subsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Prorrogas =====
IF OBJECT_ID('dbo.500_Prorrogas') IS NULL EXEC('CREATE VIEW [dbo].[500_Prorrogas] AS SELECT * FROM [dbo].[acc_sgpa_500_Prorrogas_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Aporte_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Aporte_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Aporte_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Aporte_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Cargado_HL_Det =====
IF OBJECT_ID('dbo.500_Rpt_Cargado_HL_Det') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Cargado_HL_Det] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Cargado_HL_Det_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Certificacion_UltFecha =====
IF OBJECT_ID('dbo.500_Rpt_Certificacion_UltFecha') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Certificacion_UltFecha] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Certificacion_UltFecha_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_DetalleSubsidio =====
IF OBJECT_ID('dbo.500_Rpt_DetalleSubsidio') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_DetalleSubsidio] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Imponible_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Imponible_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Imponible_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Imponible_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_NoCargadoHL =====
IF OBJECT_ID('dbo.500_Rpt_NoCargadoHL') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_NoCargadoHL] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_NoCargadoHL_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Prestacion =====
IF OBJECT_ID('dbo.500_Rpt_Prestacion') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Prestacion] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Prestacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_PrimaFallecimiento_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_PrimaFallecimiento_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_PrimaFallecimiento_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_PrimaFallecimiento_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_ReintegroMutual =====
IF OBJECT_ID('dbo.500_Rpt_ReintegroMutual') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_ReintegroMutual] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_ReintegroMutual_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_ResumenSubsidio =====
IF OBJECT_ID('dbo.500_Rpt_ResumenSubsidio') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_ResumenSubsidio] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_ResumenSubsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Trabaja_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Trabaja_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Trabaja_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Trabaja_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 506_Rpt_LiquidacionBPS =====
IF OBJECT_ID('dbo.506_Rpt_LiquidacionBPS') IS NULL EXEC('CREATE FUNCTION [dbo].[506_Rpt_LiquidacionBPS](@pMes INT, @pAnio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 506_Rpt_Subsidio =====
IF OBJECT_ID('dbo.506_Rpt_Subsidio') IS NULL EXEC('CREATE VIEW [dbo].[506_Rpt_Subsidio] AS SELECT * FROM [dbo].[acc_sgpa_506_Rpt_Subsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 506_TotalLiquidoBPSCIMes =====
IF OBJECT_ID('dbo.506_TotalLiquidoBPSCIMes') IS NULL EXEC('CREATE FUNCTION [dbo].[506_TotalLiquidoBPSCIMes](@pCI INT, @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_506_TotalLiquidoBPSCIMes_q](@pCI, @pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 601_Rpt_Recibo =====
IF OBJECT_ID('dbo.601_Rpt_Recibo') IS NULL EXEC('CREATE VIEW [dbo].[601_Rpt_Recibo] AS SELECT * FROM [dbo].[acc_sgpa_601_Rpt_Recibo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 765_CertificacionContinua =====
IF OBJECT_ID('dbo.765_CertificacionContinua') IS NULL EXEC('CREATE FUNCTION [dbo].[765_CertificacionContinua](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_765_CertificacionContinua_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 765_CertificacionEmpalma =====
IF OBJECT_ID('dbo.765_CertificacionEmpalma') IS NULL EXEC('CREATE FUNCTION [dbo].[765_CertificacionEmpalma](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_765_CertificacionEmpalma_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_AfiliadoImponible_Mes =====
IF OBJECT_ID('dbo.800_AfiliadoImponible_Mes') IS NULL EXEC('CREATE FUNCTION [dbo].[800_AfiliadoImponible_Mes](@pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_AfiliadoImponible_Mes_Fecha =====
IF OBJECT_ID('dbo.800_AfiliadoImponible_Mes_Fecha') IS NULL EXEC('CREATE FUNCTION [dbo].[800_AfiliadoImponible_Mes_Fecha](@pCodEmpresa INT, @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_AfiliadoImponible_Mes_Todos =====
IF OBJECT_ID('dbo.800_AfiliadoImponible_Mes_Todos') IS NULL EXEC('CREATE VIEW [dbo].[800_AfiliadoImponible_Mes_Todos] AS SELECT * FROM [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Todos_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_Afiliado_Imponible_Mes_Fecha =====
IF OBJECT_ID('dbo.800_Afiliado_Imponible_Mes_Fecha') IS NULL EXEC('CREATE FUNCTION [dbo].[800_Afiliado_Imponible_Mes_Fecha](@pCodEmpresa INT, @pMes NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_800_Afiliado_Imponible_Mes_Fecha_q](@pCodEmpresa, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_Cantidad_Empresa =====
IF OBJECT_ID('dbo.800_Cantidad_Empresa') IS NULL EXEC('CREATE FUNCTION [dbo].[800_Cantidad_Empresa](@pCodEmpresa INT, @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_800_Cantidad_Empresa_q](@pCodEmpresa, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 800_Cantidad_Otras =====
IF OBJECT_ID('dbo.800_Cantidad_Otras') IS NULL EXEC('CREATE FUNCTION [dbo].[800_Cantidad_Otras](@pCodEmpresa INT, @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_800_Cantidad_Otras_q](@pCodEmpresa, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 805_Activos =====
IF OBJECT_ID('dbo.805_Activos') IS NULL EXEC('CREATE VIEW [dbo].[805_Activos] AS SELECT * FROM [dbo].[acc_sgpa_805_Activos_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 805_CertificacionesxAnio =====
IF OBJECT_ID('dbo.805_CertificacionesxAnio') IS NULL EXEC('CREATE FUNCTION [dbo].[805_CertificacionesxAnio](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_805_CertificacionesxAno_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 805_CertificadosActivos =====
IF OBJECT_ID('dbo.805_CertificadosActivos') IS NULL EXEC('CREATE FUNCTION [dbo].[805_CertificadosActivos](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_805_CertificadosActivos_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 805_CertificadosActivos_Original =====
IF OBJECT_ID('dbo.805_CertificadosActivos_Original') IS NULL EXEC('CREATE FUNCTION [dbo].[805_CertificadosActivos_Original](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_805_CertificadosActivos_Original_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 805_Certificados_AnioCI =====
IF OBJECT_ID('dbo.805_Certificados_AnioCI') IS NULL EXEC('CREATE FUNCTION [dbo].[805_Certificados_AnioCI](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_805_Certificados_AnoCI_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 806_CertificadosEntre =====
IF OBJECT_ID('dbo.806_CertificadosEntre') IS NULL EXEC('CREATE FUNCTION [dbo].[806_CertificadosEntre](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_806_CertificadosEntre_q](@pAnioIni, @pAnioFin, @pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 806_CertificadosMayores =====
IF OBJECT_ID('dbo.806_CertificadosMayores') IS NULL EXEC('CREATE FUNCTION [dbo].[806_CertificadosMayores](@pAnioIni NVARCHAR(MAX), @pFechaIni NVARCHAR(MAX), @pFechaFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_806_CertificadosMayores_q](@pAnioIni, @pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 806_CertificadosMenores =====
IF OBJECT_ID('dbo.806_CertificadosMenores') IS NULL EXEC('CREATE FUNCTION [dbo].[806_CertificadosMenores](@pAnioIni NVARCHAR(MAX), @pFechaIni NVARCHAR(MAX), @pFechaFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_806_CertificadosMenores_q](@pAnioIni, @pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 806_CertificadosSexo =====
IF OBJECT_ID('dbo.806_CertificadosSexo') IS NULL EXEC('CREATE FUNCTION [dbo].[806_CertificadosSexo](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_806_CertificadosSexo_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 807_CertificadosEspecialidad =====
IF OBJECT_ID('dbo.807_CertificadosEspecialidad') IS NULL EXEC('CREATE FUNCTION [dbo].[807_CertificadosEspecialidad](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_807_CertificadosEspecialidad_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 808_CertificadosAfecciones =====
IF OBJECT_ID('dbo.808_CertificadosAfecciones') IS NULL EXEC('CREATE FUNCTION [dbo].[808_CertificadosAfecciones](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_808_CertificadosAfecciones_q](@pFechaIni, @pFechaFin, @pCodPatologia)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 808_Certificados_Cantidad =====
IF OBJECT_ID('dbo.808_Certificados_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[808_Certificados_Cantidad](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_808_Certificados_Cantidad_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 809_AfiliadoActivoFecha_Cantidad =====
IF OBJECT_ID('dbo.809_AfiliadoActivoFecha_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[809_AfiliadoActivoFecha_Cantidad](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_809_AfiliadoActivoFecha_Cantidad_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 809_AfiliadoActivo_Cantidad =====
IF OBJECT_ID('dbo.809_AfiliadoActivo_Cantidad') IS NULL EXEC('CREATE VIEW [dbo].[809_AfiliadoActivo_Cantidad] AS SELECT * FROM [dbo].[acc_sgpa_809_AfiliadoActivo_Cantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 809_AfiliadoFecha_Cantidad =====
IF OBJECT_ID('dbo.809_AfiliadoFecha_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[809_AfiliadoFecha_Cantidad](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_809_AfiliadoFecha_Cantidad_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 809_Afiliado_Cantidad =====
IF OBJECT_ID('dbo.809_Afiliado_Cantidad') IS NULL EXEC('CREATE VIEW [dbo].[809_Afiliado_Cantidad] AS SELECT * FROM [dbo].[acc_sgpa_809_Afiliado_Cantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadoActivoEntre =====
IF OBJECT_ID('dbo.810_AfiliadoActivoEntre') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadoActivoEntre](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadoActivoEntre_q](@pAnioIni, @pAnioFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadoActivoMayores =====
IF OBJECT_ID('dbo.810_AfiliadoActivoMayores') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadoActivoMayores](@pAnioIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadoActivoMayores_q](@pAnioIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadoActivoMenores =====
IF OBJECT_ID('dbo.810_AfiliadoActivoMenores') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadoActivoMenores](@pAnioIni NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadoActivoMenores_q](@pAnioIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosActivoSexo =====
IF OBJECT_ID('dbo.810_AfiliadosActivoSexo') IS NULL EXEC('CREATE VIEW [dbo].[810_AfiliadosActivoSexo] AS SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosActivoSexo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosEntre =====
IF OBJECT_ID('dbo.810_AfiliadosEntre') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosEntre](@pAnioIni NVARCHAR(MAX), @pAnioFin NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosEntre_q](@pAnioIni, @pAnioFin, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosMayores =====
IF OBJECT_ID('dbo.810_AfiliadosMayores') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosMayores](@pAnioIni NVARCHAR(MAX), @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosMayores_q](@pAnioIni, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosMenores =====
IF OBJECT_ID('dbo.810_AfiliadosMenores') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosMenores](@pAnioIni NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosMenores_q](@pAnioIni, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosSexo =====
IF OBJECT_ID('dbo.810_AfiliadosSexo') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosSexo](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosSexo_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 812_AfiliadoActivoEspecialidad =====
IF OBJECT_ID('dbo.812_AfiliadoActivoEspecialidad') IS NULL EXEC('CREATE VIEW [dbo].[812_AfiliadoActivoEspecialidad] AS SELECT * FROM [dbo].[acc_sgpa_812_AfiliadoActivoEspecialidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 812_AfiliadosEspecialidad =====
IF OBJECT_ID('dbo.812_AfiliadosEspecialidad') IS NULL EXEC('CREATE FUNCTION [dbo].[812_AfiliadosEspecialidad](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_812_AfiliadosEspecialidad_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 813_CertificacionAfeccionDistintas =====
IF OBJECT_ID('dbo.813_CertificacionAfeccionDistintas') IS NULL EXEC('CREATE FUNCTION [dbo].[813_CertificacionAfeccionDistintas](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_813_CertificacionAfeccionDistintas_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 816_Certificados_GrupoAfeccion =====
IF OBJECT_ID('dbo.816_Certificados_GrupoAfeccion') IS NULL EXEC('CREATE FUNCTION [dbo].[816_Certificados_GrupoAfeccion](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_816_Certificados_GrupoAfeccion_q](@pFechaIni, @pFechaFin, @pCodPatologia)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 817_Certificados_Patologia =====
IF OBJECT_ID('dbo.817_Certificados_Patologia') IS NULL EXEC('CREATE FUNCTION [dbo].[817_Certificados_Patologia](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_817_Certificados_Patologia_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 818_Certificados_Patologia =====
IF OBJECT_ID('dbo.818_Certificados_Patologia') IS NULL EXEC('CREATE FUNCTION [dbo].[818_Certificados_Patologia](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_818_Certificados_Patologia_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 819_Certificados_AfeccionGrupo =====
IF OBJECT_ID('dbo.819_Certificados_AfeccionGrupo') IS NULL EXEC('CREATE FUNCTION [dbo].[819_Certificados_AfeccionGrupo](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_819_Certificados_AfeccionGrupo_q](@pFechaIni, @pFechaFin, @pCodPatologia)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 820_Certificados_AfeccionTipo =====
IF OBJECT_ID('dbo.820_Certificados_AfeccionTipo') IS NULL EXEC('CREATE FUNCTION [dbo].[820_Certificados_AfeccionTipo](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0), @pCodPatologia NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_820_Certificados_AfeccionTipo_q](@pFechaIni, @pFechaFin, @pCodPatologia)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 822_AfiliadoGE =====
IF OBJECT_ID('dbo.822_AfiliadoGE') IS NULL EXEC('CREATE VIEW [dbo].[822_AfiliadoGE] AS SELECT * FROM [dbo].[acc_sgpa_822_AfiliadoGE_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 824_PrestacionesCantidad =====
IF OBJECT_ID('dbo.824_PrestacionesCantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[824_PrestacionesCantidad](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_824_PrestacionesCantidad_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 825_PrestacionesImporte_Pesos =====
IF OBJECT_ID('dbo.825_PrestacionesImporte_Pesos') IS NULL EXEC('CREATE FUNCTION [dbo].[825_PrestacionesImporte_Pesos](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_825_PrestacionesImporte_Pesos_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 826_PrestacionesImporte_Dolares =====
IF OBJECT_ID('dbo.826_PrestacionesImporte_Dolares') IS NULL EXEC('CREATE FUNCTION [dbo].[826_PrestacionesImporte_Dolares](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_826_PrestacionesImporte_Dolares_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 828_Cantidad_Empresa =====
IF OBJECT_ID('dbo.828_Cantidad_Empresa') IS NULL EXEC('CREATE FUNCTION [dbo].[828_Cantidad_Empresa](@pCodEmpresa NVARCHAR(MAX), @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_828_Cantidad_Empresa_q](@pCodEmpresa, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 828_Cantidad_Otras =====
IF OBJECT_ID('dbo.828_Cantidad_Otras') IS NULL EXEC('CREATE FUNCTION [dbo].[828_Cantidad_Otras](@pCodEmpresa INT, @pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_828_Cantidad_Otras_q](@pCodEmpresa, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 830_CantidadPorPuesto =====
IF OBJECT_ID('dbo.830_CantidadPorPuesto') IS NULL EXEC('CREATE FUNCTION [dbo].[830_CantidadPorPuesto](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_830_CantidadPorPuesto_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 830_CantidadPorPuestoNo0 =====
IF OBJECT_ID('dbo.830_CantidadPorPuestoNo0') IS NULL EXEC('CREATE FUNCTION [dbo].[830_CantidadPorPuestoNo0](@pFecha DATETIME2(0), @pMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_830_CantidadPorPuestoNo0_q](@pFecha, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 999_Excel_Tmp =====
IF OBJECT_ID('dbo.999_Excel_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[999_Excel_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_999_Excel_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 999_Parametros =====
IF OBJECT_ID('dbo.999_Parametros') IS NULL EXEC('CREATE FUNCTION [dbo].[999_Parametros](@pLogin NVARCHAR(MAX), @pClave NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_999_Parametros_q](@pLogin, @pClave)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: BpsMutualista =====
IF OBJECT_ID('dbo.BpsMutualista') IS NULL EXEC('CREATE VIEW [dbo].[BpsMutualista] AS SELECT * FROM [dbo].[acc_sgpa_BpsMutualista_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Buscar duplicados por Bps4 =====
IF OBJECT_ID('dbo.Buscar duplicados por Bps4') IS NULL EXEC('CREATE VIEW [dbo].[Buscar duplicados por Bps4] AS SELECT * FROM [dbo].[acc_sgpa_Buscar_duplicados_por_Bps4_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Buscar duplicados por zRs_AEsp =====
IF OBJECT_ID('dbo.Buscar duplicados por zRs_AEsp') IS NULL EXEC('CREATE VIEW [dbo].[Buscar duplicados por zRs_AEsp] AS SELECT * FROM [dbo].[acc_sgpa_Buscar_duplicados_por_zRs_AEsp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Afiliado =====
IF OBJECT_ID('dbo.Rpt_Afiliado') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Afiliado] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Afiliado_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Aporte =====
IF OBJECT_ID('dbo.Rpt_Aporte') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Aporte] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Aporte_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Certificacion =====
IF OBJECT_ID('dbo.Rpt_Certificacion') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Certificacion] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Certificacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Certificacion2 =====
IF OBJECT_ID('dbo.Rpt_Certificacion2') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Certificacion2] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Certificacion2_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Discount =====
IF OBJECT_ID('dbo.Rpt_Discount') IS NULL EXEC('CREATE FUNCTION [dbo].[Rpt_Discount](@pCodDiscount NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pFecha DATETIME2(0), @pCodBanco NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_Rpt_Discount_q](@pCodDiscount, @pLiquidar, @pMes, @pAnio, @pFecha, @pCodBanco)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Imponible =====
IF OBJECT_ID('dbo.Rpt_Imponible') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Imponible] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Imponible_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Imponible_Activo =====
IF OBJECT_ID('dbo.Rpt_Imponible_Activo') IS NULL EXEC('CREATE FUNCTION [dbo].[Rpt_Imponible_Activo](@pMesIni NVARCHAR(MAX), @pMesFin NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_Rpt_Imponible_Activo_q](@pMesIni, @pMesFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Mutualista =====
IF OBJECT_ID('dbo.Rpt_Mutualista') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Mutualista] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Mutualista_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Prestacion =====
IF OBJECT_ID('dbo.Rpt_Prestacion') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Prestacion] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Prestacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_ReintegroMutual =====
IF OBJECT_ID('dbo.Rpt_ReintegroMutual') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_ReintegroMutual] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_ReintegroMutual_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_SubsidioCabezal =====
IF OBJECT_ID('dbo.Rpt_SubsidioCabezal') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_SubsidioCabezal] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_SubsidioCabezal_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Trabaja =====
IF OBJECT_ID('dbo.Rpt_Trabaja') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Trabaja] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Trabaja_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Trabaja_Rpt =====
IF OBJECT_ID('dbo.Rpt_Trabaja_Rpt') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Trabaja_Rpt] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Trabaja_Rpt_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: rsAfiliadoActivo =====
IF OBJECT_ID('dbo.rsAfiliadoActivo') IS NULL EXEC('CREATE VIEW [dbo].[rsAfiliadoActivo] AS SELECT * FROM [dbo].[acc_sgpa_rsAfiliadoActivo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfeccionGrupo =====
IF OBJECT_ID('dbo.Rs_AfeccionGrupo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfeccionGrupo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfeccionGrupo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfeccionGrupo_Desc =====
IF OBJECT_ID('dbo.Rs_AfeccionGrupo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfeccionGrupo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfeccionGrupo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfeccionTipo =====
IF OBJECT_ID('dbo.Rs_AfeccionTipo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfeccionTipo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfeccionTipo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfeccionTipo_Desc =====
IF OBJECT_ID('dbo.Rs_AfeccionTipo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfeccionTipo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfeccionTipo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Afiliado =====
IF OBJECT_ID('dbo.Rs_Afiliado') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Afiliado] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Afiliado_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoApunte =====
IF OBJECT_ID('dbo.Rs_AfiliadoApunte') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoApunte] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoApunte_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoApunteFromPeriodo =====
IF OBJECT_ID('dbo.Rs_AfiliadoApunteFromPeriodo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoApunteFromPeriodo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoApunteFromPeriodo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoCristalin =====
IF OBJECT_ID('dbo.Rs_AfiliadoCristalin') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoCristalin] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoCristalin_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoEspecialidad =====
IF OBJECT_ID('dbo.Rs_AfiliadoEspecialidad') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoEspecialidad] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoEspecialidadDesc =====
IF OBJECT_ID('dbo.Rs_AfiliadoEspecialidadDesc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoEspecialidadDesc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoEspecialidadDesc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoEspecialidad_Desc =====
IF OBJECT_ID('dbo.Rs_AfiliadoEspecialidad_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoEspecialidad_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoImponibleMes =====
IF OBJECT_ID('dbo.Rs_AfiliadoImponibleMes') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoImponibleMes] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoImponibleMes_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AfiliadoImponibleMesNoBaja =====
IF OBJECT_ID('dbo.Rs_AfiliadoImponibleMesNoBaja') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AfiliadoImponibleMesNoBaja] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Afiliado_Desc =====
IF OBJECT_ID('dbo.Rs_Afiliado_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Afiliado_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Afiliado_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Afiliado_FechaNacimiento =====
IF OBJECT_ID('dbo.Rs_Afiliado_FechaNacimiento') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Afiliado_FechaNacimiento] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Afiliado_FechaNacimiento_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AporteTipo =====
IF OBJECT_ID('dbo.Rs_AporteTipo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AporteTipo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AporteTipo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_AporteTipo_Desc =====
IF OBJECT_ID('dbo.Rs_AporteTipo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_AporteTipo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_AporteTipo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_BajaMotivo =====
IF OBJECT_ID('dbo.Rs_BajaMotivo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_BajaMotivo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_BajaMotivo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_BajaMotivo_Desc =====
IF OBJECT_ID('dbo.Rs_BajaMotivo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_BajaMotivo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_BajaMotivo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Banco_Desc =====
IF OBJECT_ID('dbo.Rs_Banco_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Banco_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Banco_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Bps2 =====
IF OBJECT_ID('dbo.Rs_Bps2') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Bps2] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Bps2_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Bps3 =====
IF OBJECT_ID('dbo.Rs_Bps3') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Bps3] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Bps3_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Bps4 =====
IF OBJECT_ID('dbo.Rs_Bps4') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Bps4] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Bps4_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_CaseCasm =====
IF OBJECT_ID('dbo.Rs_CaseCasm') IS NULL EXEC('CREATE VIEW [dbo].[Rs_CaseCasm] AS SELECT * FROM [dbo].[acc_sgpa_Rs_CaseCasm_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Certificacion =====
IF OBJECT_ID('dbo.Rs_Certificacion') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Certificacion] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Certificacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Certificacion_Nombre =====
IF OBJECT_ID('dbo.Rs_Certificacion_Nombre') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Certificacion_Nombre] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Certificacion_Nombre_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Certificador =====
IF OBJECT_ID('dbo.Rs_Certificador') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Certificador] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Certificador_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Certificador_Desc =====
IF OBJECT_ID('dbo.Rs_Certificador_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Certificador_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Certificador_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Cristalin =====
IF OBJECT_ID('dbo.Rs_Cristalin') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Cristalin] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Cristalin_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Departamento_Desc =====
IF OBJECT_ID('dbo.Rs_Departamento_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Departamento_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Departamento_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Empleo =====
IF OBJECT_ID('dbo.Rs_Empleo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Empleo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Empleo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Empresa =====
IF OBJECT_ID('dbo.Rs_Empresa') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Empresa] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Empresa_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_EmpresaPago =====
IF OBJECT_ID('dbo.Rs_EmpresaPago') IS NULL EXEC('CREATE VIEW [dbo].[Rs_EmpresaPago] AS SELECT * FROM [dbo].[acc_sgpa_Rs_EmpresaPago_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Empresa_Desc =====
IF OBJECT_ID('dbo.Rs_Empresa_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Empresa_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Empresa_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Empresa_Desc_Real =====
IF OBJECT_ID('dbo.Rs_Empresa_Desc_Real') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Empresa_Desc_Real] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Empresa_Desc_Real_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Especialidad_Desc =====
IF OBJECT_ID('dbo.Rs_Especialidad_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Especialidad_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Especialidad_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Export_BROU =====
IF OBJECT_ID('dbo.Rs_Export_BROU') IS NULL EXEC('CREATE FUNCTION [dbo].[Rs_Export_BROU](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_Rs_Export_BROU_q](@pMes, @pAnio, @pLiquidar, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Export_NBC =====
IF OBJECT_ID('dbo.Rs_Export_NBC') IS NULL EXEC('CREATE FUNCTION [dbo].[Rs_Export_NBC](@pMes NVARCHAR(MAX), @pAnio NVARCHAR(MAX), @pLiquidar NVARCHAR(MAX), @pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_Rs_Export_NBC_q](@pMes, @pAnio, @pLiquidar, @pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_FormaPago =====
IF OBJECT_ID('dbo.Rs_FormaPago') IS NULL EXEC('CREATE VIEW [dbo].[Rs_FormaPago] AS SELECT * FROM [dbo].[acc_sgpa_Rs_FormaPago_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_FormaPago_Desc =====
IF OBJECT_ID('dbo.Rs_FormaPago_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_FormaPago_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_FormaPago_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_GrupoEtario_Descrip =====
IF OBJECT_ID('dbo.Rs_GrupoEtario_Descrip') IS NULL EXEC('CREATE VIEW [dbo].[Rs_GrupoEtario_Descrip] AS SELECT * FROM [dbo].[acc_sgpa_Rs_GrupoEtario_Descrip_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_ImpMax =====
IF OBJECT_ID('dbo.Rs_ImpMax') IS NULL EXEC('CREATE VIEW [dbo].[Rs_ImpMax] AS SELECT * FROM [dbo].[acc_sgpa_Rs_ImpMax_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Imponible =====
IF OBJECT_ID('dbo.Rs_Imponible') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Imponible] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Imponible_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Imponible_Ult =====
IF OBJECT_ID('dbo.Rs_Imponible_Ult') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Imponible_Ult] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Imponible_Ult_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Imponible_Ult_Ant =====
IF OBJECT_ID('dbo.Rs_Imponible_Ult_Ant') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Imponible_Ult_Ant] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Imponible_Ult_Ant_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_InformeEstadistico =====
IF OBJECT_ID('dbo.Rs_InformeEstadistico') IS NULL EXEC('CREATE VIEW [dbo].[Rs_InformeEstadistico] AS SELECT * FROM [dbo].[acc_sgpa_Rs_InformeEstadistico_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_MaxImp_Afiliado =====
IF OBJECT_ID('dbo.Rs_MaxImp_Afiliado') IS NULL EXEC('CREATE VIEW [dbo].[Rs_MaxImp_Afiliado] AS SELECT * FROM [dbo].[acc_sgpa_Rs_MaxImp_Afiliado_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Mutualista =====
IF OBJECT_ID('dbo.Rs_Mutualista') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Mutualista] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Mutualista_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Mutualista_Desc =====
IF OBJECT_ID('dbo.Rs_Mutualista_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Mutualista_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Mutualista_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Patologia =====
IF OBJECT_ID('dbo.Rs_Patologia') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Patologia] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Patologia_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Patologia_Desc =====
IF OBJECT_ID('dbo.Rs_Patologia_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Patologia_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Patologia_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_Prestacion =====
IF OBJECT_ID('dbo.Rs_Prestacion') IS NULL EXEC('CREATE VIEW [dbo].[Rs_Prestacion] AS SELECT * FROM [dbo].[acc_sgpa_Rs_Prestacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestacionTipo =====
IF OBJECT_ID('dbo.Rs_PrestacionTipo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestacionTipo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_PrestacionTipo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestacionTipo_Desc =====
IF OBJECT_ID('dbo.Rs_PrestacionTipo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestacionTipo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_PrestacionTipo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_RegimenAporte_Desc =====
IF OBJECT_ID('dbo.Rs_RegimenAporte_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_RegimenAporte_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_RegimenAporte_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_RegimenJubilatorio =====
IF OBJECT_ID('dbo.Rs_RegimenJubilatorio') IS NULL EXEC('CREATE VIEW [dbo].[Rs_RegimenJubilatorio] AS SELECT * FROM [dbo].[acc_sgpa_Rs_RegimenJubilatorio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_RegimenJubilatorio_Desc =====
IF OBJECT_ID('dbo.Rs_RegimenJubilatorio_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_RegimenJubilatorio_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_RegimenJubilatorio_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_ReintegroMutual =====
IF OBJECT_ID('dbo.Rs_ReintegroMutual') IS NULL EXEC('CREATE VIEW [dbo].[Rs_ReintegroMutual] AS SELECT * FROM [dbo].[acc_sgpa_Rs_ReintegroMutual_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SalidaTipo =====
IF OBJECT_ID('dbo.Rs_SalidaTipo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SalidaTipo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SalidaTipo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SalidaTipo_Desc =====
IF OBJECT_ID('dbo.Rs_SalidaTipo_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SalidaTipo_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SalidaTipo_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SituacionMutual =====
IF OBJECT_ID('dbo.Rs_SituacionMutual') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SituacionMutual] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SituacionMutual_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SituacionMutual_Desc =====
IF OBJECT_ID('dbo.Rs_SituacionMutual_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SituacionMutual_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SituacionMutual_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SituacionPago =====
IF OBJECT_ID('dbo.Rs_SituacionPago') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SituacionPago] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SituacionPago_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SituacionPago_Desc =====
IF OBJECT_ID('dbo.Rs_SituacionPago_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SituacionPago_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SituacionPago_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SubsidioItem =====
IF OBJECT_ID('dbo.Rs_SubsidioItem') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SubsidioItem] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SubsidioItem_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SubsidioItemCodXCI =====
IF OBJECT_ID('dbo.Rs_SubsidioItemCodXCI') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SubsidioItemCodXCI] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SubsidioItemCodXCI_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SubsidioItemCod_Afiliado =====
IF OBJECT_ID('dbo.Rs_SubsidioItemCod_Afiliado') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SubsidioItemCod_Afiliado] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SubsidioItemCod_Afiliado_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SubsidioItemCod_Desc =====
IF OBJECT_ID('dbo.Rs_SubsidioItemCod_Desc') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SubsidioItemCod_Desc] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SubsidioItemCod_Desc_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_SubsidioXMes =====
IF OBJECT_ID('dbo.Rs_SubsidioXMes') IS NULL EXEC('CREATE VIEW [dbo].[Rs_SubsidioXMes] AS SELECT * FROM [dbo].[acc_sgpa_Rs_SubsidioXMes_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_TrabajaActivo =====
IF OBJECT_ID('dbo.Rs_TrabajaActivo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_TrabajaActivo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_TrabajaActivo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_TrabajaUltimo =====
IF OBJECT_ID('dbo.Rs_TrabajaUltimo') IS NULL EXEC('CREATE VIEW [dbo].[Rs_TrabajaUltimo] AS SELECT * FROM [dbo].[acc_sgpa_Rs_TrabajaUltimo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xEmpresaCantidad =====
IF OBJECT_ID('dbo.xEmpresaCantidad') IS NULL EXEC('CREATE VIEW [dbo].[xEmpresaCantidad] AS SELECT * FROM [dbo].[acc_sgpa_xEmpresaCantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xEmpresaPromedio =====
IF OBJECT_ID('dbo.xEmpresaPromedio') IS NULL EXEC('CREATE VIEW [dbo].[xEmpresaPromedio] AS SELECT * FROM [dbo].[acc_sgpa_xEmpresaPromedio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xEmpresaPromedioTodo =====
IF OBJECT_ID('dbo.xEmpresaPromedioTodo') IS NULL EXEC('CREATE VIEW [dbo].[xEmpresaPromedioTodo] AS SELECT * FROM [dbo].[acc_sgpa_xEmpresaPromedioTodo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xMutualistaCantidad =====
IF OBJECT_ID('dbo.xMutualistaCantidad') IS NULL EXEC('CREATE VIEW [dbo].[xMutualistaCantidad] AS SELECT * FROM [dbo].[acc_sgpa_xMutualistaCantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xSinImponiblexEmpresa =====
IF OBJECT_ID('dbo.xSinImponiblexEmpresa') IS NULL EXEC('CREATE VIEW [dbo].[xSinImponiblexEmpresa] AS SELECT * FROM [dbo].[acc_sgpa_xSinImponiblexEmpresa_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xw_Suma_ValorJornal1 =====
IF OBJECT_ID('dbo.xw_Suma_ValorJornal1') IS NULL EXEC('CREATE VIEW [dbo].[xw_Suma_ValorJornal1] AS SELECT * FROM [dbo].[acc_sgpa_xw_Suma_ValorJornal1_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: zGrupoSubsidio =====
IF OBJECT_ID('dbo.zGrupoSubsidio') IS NULL EXEC('CREATE VIEW [dbo].[zGrupoSubsidio] AS SELECT * FROM [dbo].[acc_sgpa_zGrupoSubsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 200_Imponible_6_Meses =====
IF OBJECT_ID('dbo.200_Imponible_6_Meses') IS NULL EXEC('CREATE FUNCTION [dbo].[200_Imponible_6_Meses](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 200_Imponible_Ult_Mes =====
IF OBJECT_ID('dbo.200_Imponible_Ult_Mes') IS NULL EXEC('CREATE FUNCTION [dbo].[200_Imponible_Ult_Mes](@pMes INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 220_AfiliadoPromedio =====
IF OBJECT_ID('dbo.220_AfiliadoPromedio') IS NULL EXEC('CREATE FUNCTION [dbo].[220_AfiliadoPromedio](@pCI INT, @pMes INT, @pMesIni INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_220_AfiliadoPromedio_q](@pCI, @pMes, @pMesIni)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 250_Control_Aporte =====
IF OBJECT_ID('dbo.250_Control_Aporte') IS NULL EXEC('CREATE FUNCTION [dbo].[250_Control_Aporte](@pFecha DATETIME2(0), @pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_250_Control_Aporte_q](@pFecha, @pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Subsidio =====
IF OBJECT_ID('dbo.500_Rpt_Subsidio') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Subsidio] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Subsidio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_AfiliadoDiasImporte =====
IF OBJECT_ID('dbo.300_AfiliadoDiasImporte') IS NULL EXEC('CREATE FUNCTION [dbo].[300_AfiliadoDiasImporte](@pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Promedio_Mes =====
IF OBJECT_ID('dbo.400_Promedio_Mes') IS NULL EXEC('CREATE VIEW [dbo].[400_Promedio_Mes] AS SELECT * FROM [dbo].[acc_sgpa_400_Promedio_Mes_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Promedio_Mes_Puesto =====
IF OBJECT_ID('dbo.400_Promedio_Mes_Puesto') IS NULL EXEC('CREATE VIEW [dbo].[400_Promedio_Mes_Puesto] AS SELECT * FROM [dbo].[acc_sgpa_400_Promedio_Mes_Puesto_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 460_AfiliadoPromedioxCI =====
IF OBJECT_ID('dbo.460_AfiliadoPromedioxCI') IS NULL EXEC('CREATE FUNCTION [dbo].[460_AfiliadoPromedioxCI](@pCI INT, @pAnioMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_460_AfiliadoPromedioxCI_q](@pCI, @pAnioMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_Rpt_Ficha_Certificacion =====
IF OBJECT_ID('dbo.480_Rpt_Ficha_Certificacion') IS NULL EXEC('CREATE VIEW [dbo].[480_Rpt_Ficha_Certificacion] AS SELECT * FROM [dbo].[acc_sgpa_480_Rpt_Ficha_Certificacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_Prorrogas =====
IF OBJECT_ID('dbo.480_Prorrogas') IS NULL EXEC('CREATE VIEW [dbo].[480_Prorrogas] AS SELECT * FROM [dbo].[acc_sgpa_480_Prorrogas_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 490_SubsidioImporte =====
IF OBJECT_ID('dbo.490_SubsidioImporte') IS NULL EXEC('CREATE VIEW [dbo].[490_SubsidioImporte] AS SELECT * FROM [dbo].[acc_sgpa_490_SubsidioImporte_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Cargado_HL =====
IF OBJECT_ID('dbo.500_Rpt_Cargado_HL') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Cargado_HL] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Cargado_HL_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_DiasCertificacion =====
IF OBJECT_ID('dbo.500_Rpt_DiasCertificacion') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_DiasCertificacion] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_DiasCertificacion_S =====
IF OBJECT_ID('dbo.500_Rpt_DiasCertificacion_S') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_DiasCertificacion_S] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_S_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_DetalleSubsidio_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_DetalleSubsidio_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_DetalleSubsidio_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 506_Export_SubsidioConBPS =====
IF OBJECT_ID('dbo.506_Export_SubsidioConBPS') IS NULL EXEC('CREATE FUNCTION [dbo].[506_Export_SubsidioConBPS](@pMes INT, @pAnio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_506_Export_SubsidioConBPS_q](@pMes, @pAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 756_NoBaja =====
IF OBJECT_ID('dbo.756_NoBaja') IS NULL EXEC('CREATE FUNCTION [dbo].[756_NoBaja](@pFecha DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_756_NoBaja_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 815_AfiliadoImponible =====
IF OBJECT_ID('dbo.815_AfiliadoImponible') IS NULL EXEC('CREATE FUNCTION [dbo].[815_AfiliadoImponible](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_815_AfiliadoImponible_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_CI_Todos =====
IF OBJECT_ID('dbo.801_CI_Todos') IS NULL EXEC('CREATE FUNCTION [dbo].[801_CI_Todos](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_CI_Todos_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_Promedio_Ult6 =====
IF OBJECT_ID('dbo.801_Promedio_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[801_Promedio_Ult6](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_Promedio_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_Promedio_UltMes =====
IF OBJECT_ID('dbo.801_Promedio_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[801_Promedio_UltMes](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_Promedio_UltMes_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>0_Ult6 =====
IF OBJECT_ID('dbo.802_>0_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>0_Ult6](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>0_UltMes =====
IF OBJECT_ID('dbo.802_>0_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>0_UltMes](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__0_UltMes_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_Ult6 =====
IF OBJECT_ID('dbo.802_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[802_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_UltMes =====
IF OBJECT_ID('dbo.802_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[802_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 803_Ult6 =====
IF OBJECT_ID('dbo.803_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[803_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_803_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 803_UltMes =====
IF OBJECT_ID('dbo.803_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[803_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_803_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>0_Ult6 =====
IF OBJECT_ID('dbo.804_>0_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>0_Ult6](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>0_UltMes =====
IF OBJECT_ID('dbo.804_>0_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>0_UltMes](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_Ult6 =====
IF OBJECT_ID('dbo.804_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[804_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_UltMes =====
IF OBJECT_ID('dbo.804_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[804_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 811_Afiliado<125_Pct_Ult6 =====
IF OBJECT_ID('dbo.811_Afiliado<125_Pct_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[811_Afiliado<125_Pct_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 811_Afiliado<125_Pct_UltMes =====
IF OBJECT_ID('dbo.811_Afiliado<125_Pct_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[811_Afiliado<125_Pct_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 814_AfiliadoImponibleFranja =====
IF OBJECT_ID('dbo.814_AfiliadoImponibleFranja') IS NULL EXEC('CREATE FUNCTION [dbo].[814_AfiliadoImponibleFranja](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_814_AfiliadoImponibleFranja_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 813_CertificadosAfeccion =====
IF OBJECT_ID('dbo.813_CertificadosAfeccion') IS NULL EXEC('CREATE FUNCTION [dbo].[813_CertificadosAfeccion](@pCodPatologia NVARCHAR(MAX), @pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_813_CertificadosAfeccion_q](@pCodPatologia, @pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 806_CertificadosCantidad =====
IF OBJECT_ID('dbo.806_CertificadosCantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[806_CertificadosCantidad](@pFechaIni DATETIME2(0), @pFechaFin DATETIME2(0))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_806_CertificadosCantidad_q](@pFechaIni, @pFechaFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosActivos_GE =====
IF OBJECT_ID('dbo.810_AfiliadosActivos_GE') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosActivos_GE](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosActivos_GE_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 810_AfiliadosActivos_GE2 =====
IF OBJECT_ID('dbo.810_AfiliadosActivos_GE2') IS NULL EXEC('CREATE FUNCTION [dbo].[810_AfiliadosActivos_GE2](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_810_AfiliadosActivos_GE2_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 827_AfiliadosActivos_GE_Sexo =====
IF OBJECT_ID('dbo.827_AfiliadosActivos_GE_Sexo') IS NULL EXEC('CREATE FUNCTION [dbo].[827_AfiliadosActivos_GE_Sexo](@pFecha NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_827_AfiliadosActivos_GE_Sexo_q](@pFecha)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 830_Rpt_Cantidad_Por_Puesto =====
IF OBJECT_ID('dbo.830_Rpt_Cantidad_Por_Puesto') IS NULL EXEC('CREATE FUNCTION [dbo].[830_Rpt_Cantidad_Por_Puesto](@pFecha DATETIME2(0), @pMes INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_830_Rpt_Cantidad_Por_Puesto_q](@pFecha, @pMes)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Afiliado_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Afiliado_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Afiliado_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Afiliado_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_AfiliadoFormatCI =====
IF OBJECT_ID('dbo.Rpt_AfiliadoFormatCI') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_AfiliadoFormatCI] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_AfiliadoFormatCI_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_AfiliadoNOFormatCI =====
IF OBJECT_ID('dbo.Rpt_AfiliadoNOFormatCI') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_AfiliadoNOFormatCI] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_AfiliadoNOFormatCI_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Certificado_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Certificado_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Certificado_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Certificado_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_DiasCertificacion_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_DiasCertificacion_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_DiasCertificacion_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_Mutualista_Tmp =====
IF OBJECT_ID('dbo.500_Rpt_Mutualista_Tmp') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_Mutualista_Tmp] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_Mutualista_Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 250_AfiliadoConDerecho =====
IF OBJECT_ID('dbo.250_AfiliadoConDerecho') IS NULL EXEC('CREATE FUNCTION [dbo].[250_AfiliadoConDerecho](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_Promedio =====
IF OBJECT_ID('dbo.480_Promedio') IS NULL EXEC('CREATE VIEW [dbo].[480_Promedio] AS SELECT * FROM [dbo].[acc_sgpa_480_Promedio_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 320_AfiliadoPromedio =====
IF OBJECT_ID('dbo.320_AfiliadoPromedio') IS NULL EXEC('CREATE FUNCTION [dbo].[320_AfiliadoPromedio](@pCI INT, @pMesAnioIni INT, @pMesAnioFin INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_320_AfiliadoPromedio_q](@pCI, @pMesAnioIni, @pMesAnioFin)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 320_AfiliadoUltMes =====
IF OBJECT_ID('dbo.320_AfiliadoUltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[320_AfiliadoUltMes](@pCI INT, @pMesAnio INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_320_AfiliadoUltMes_q](@pCI, @pMesAnio)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 100_Create_Bps4Tmp =====
IF OBJECT_ID('dbo.100_Create_Bps4Tmp') IS NULL EXEC('CREATE VIEW [dbo].[100_Create_Bps4Tmp] AS SELECT * FROM [dbo].[acc_sgpa_100_Create_Bps4Tmp_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 480_Certificacion =====
IF OBJECT_ID('dbo.480_Certificacion') IS NULL EXEC('CREATE VIEW [dbo].[480_Certificacion] AS SELECT * FROM [dbo].[acc_sgpa_480_Certificacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 510_Rpt_Trabaja =====
IF OBJECT_ID('dbo.510_Rpt_Trabaja') IS NULL EXEC('CREATE VIEW [dbo].[510_Rpt_Trabaja] AS SELECT * FROM [dbo].[acc_sgpa_510_Rpt_Trabaja_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_Trabaja_DBG =====
IF OBJECT_ID('dbo.Rpt_Trabaja_DBG') IS NULL EXEC('CREATE VIEW [dbo].[Rpt_Trabaja_DBG] AS SELECT * FROM [dbo].[acc_sgpa_Rpt_Trabaja_DBG_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 150_5_Mejores_Pagos =====
IF OBJECT_ID('dbo.150_5_Mejores_Pagos') IS NULL EXEC('CREATE VIEW [dbo].[150_5_Mejores_Pagos] AS SELECT * FROM [dbo].[acc_sgpa_150_5_Mejores_Pagos_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: acc_sgpa_300_SubsidioItemCod_Full_Data_q =====
IF OBJECT_ID('dbo.acc_sgpa_300_SubsidioItemCod_Full_Data_q') IS NULL EXEC('CREATE FUNCTION [dbo].[acc_sgpa_300_SubsidioItemCod_Full_Data_q](@pFecha DATETIME2(0), @pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_acc_sgpa_300_SubsidioItemCod_Full_Data_q_q](@pFecha, @pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_CertificadoEmpresa_S =====
IF OBJECT_ID('dbo.500_Rpt_CertificadoEmpresa_S') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_CertificadoEmpresa_S] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa_S_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 505_Temporal =====
IF OBJECT_ID('dbo.505_Temporal') IS NULL EXEC('CREATE FUNCTION [dbo].[505_Temporal](@pMes NVARCHAR(MAX), @pAno NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_505_Temporal_q](@pMes, @pAno)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_Promedio_Ult6_Todos =====
IF OBJECT_ID('dbo.801_Promedio_Ult6_Todos') IS NULL EXEC('CREATE FUNCTION [dbo].[801_Promedio_Ult6_Todos](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_Promedio_Ult6_Todos_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: xRptEmpresaCantidad =====
IF OBJECT_ID('dbo.xRptEmpresaCantidad') IS NULL EXEC('CREATE VIEW [dbo].[xRptEmpresaCantidad] AS SELECT * FROM [dbo].[acc_sgpa_xRptEmpresaCantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xRptPromedioEmpresa =====
IF OBJECT_ID('dbo.xRptPromedioEmpresa') IS NULL EXEC('CREATE VIEW [dbo].[xRptPromedioEmpresa] AS SELECT * FROM [dbo].[acc_sgpa_xRptPromedioEmpresa_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xRptEmpresaPromedioTodo =====
IF OBJECT_ID('dbo.xRptEmpresaPromedioTodo') IS NULL EXEC('CREATE VIEW [dbo].[xRptEmpresaPromedioTodo] AS SELECT * FROM [dbo].[acc_sgpa_xRptEmpresaPromedioTodo_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xRptMutualistaCantidad =====
IF OBJECT_ID('dbo.xRptMutualistaCantidad') IS NULL EXEC('CREATE VIEW [dbo].[xRptMutualistaCantidad] AS SELECT * FROM [dbo].[acc_sgpa_xRptMutualistaCantidad_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xEmpresaPromedioNo0 =====
IF OBJECT_ID('dbo.xEmpresaPromedioNo0') IS NULL EXEC('CREATE VIEW [dbo].[xEmpresaPromedioNo0] AS SELECT * FROM [dbo].[acc_sgpa_xEmpresaPromedioNo0_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 200_PagarMutualista =====
IF OBJECT_ID('dbo.200_PagarMutualista') IS NULL EXEC('CREATE FUNCTION [dbo].[200_PagarMutualista](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 201_PagarMutualista =====
IF OBJECT_ID('dbo.201_PagarMutualista') IS NULL EXEC('CREATE FUNCTION [dbo].[201_PagarMutualista](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_201_PagarMutualista_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_AfiliadoValorJornal =====
IF OBJECT_ID('dbo.300_AfiliadoValorJornal') IS NULL EXEC('CREATE FUNCTION [dbo].[300_AfiliadoValorJornal](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_AfiliadoValorJornal_q](@pCodCasemed, @pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_AfiliadoValorJornalCasemed =====
IF OBJECT_ID('dbo.300_AfiliadoValorJornalCasemed') IS NULL EXEC('CREATE FUNCTION [dbo].[300_AfiliadoValorJornalCasemed](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_AfiliadoValorJornalCasemed_q](@pCodCasemed, @pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_AfiliadoValorJornalxEmpresa =====
IF OBJECT_ID('dbo.300_AfiliadoValorJornalxEmpresa') IS NULL EXEC('CREATE FUNCTION [dbo].[300_AfiliadoValorJornalxEmpresa](@pCodCasemed INT, @pCI INT, @pMesIni INT, @pMesFin INT, @pLiquidar NVARCHAR(MAX), @pDias NVARCHAR(MAX), @pMes NVARCHAR(MAX), @pMesIniImp INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_AfiliadoValorJornalxEmpresa_q](@pCodCasemed, @pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Promedio_Gral =====
IF OBJECT_ID('dbo.400_Promedio_Gral') IS NULL EXEC('CREATE VIEW [dbo].[400_Promedio_Gral] AS SELECT * FROM [dbo].[acc_sgpa_400_Promedio_Gral_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 400_Promedio_Gral_Puesto =====
IF OBJECT_ID('dbo.400_Promedio_Gral_Puesto') IS NULL EXEC('CREATE VIEW [dbo].[400_Promedio_Gral_Puesto] AS SELECT * FROM [dbo].[acc_sgpa_400_Promedio_Gral_Puesto_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_Cantidad_Todos =====
IF OBJECT_ID('dbo.801_Cantidad_Todos') IS NULL EXEC('CREATE FUNCTION [dbo].[801_Cantidad_Todos](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_Cantidad_Todos_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_Ult6_Cantidad =====
IF OBJECT_ID('dbo.801_Ult6_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[801_Ult6_Cantidad](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_Ult6_Cantidad_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 801_UltMes_Cantidad =====
IF OBJECT_ID('dbo.801_UltMes_Cantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[801_UltMes_Cantidad](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_801_UltMes_Cantidad_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>0_Cantidad_Ult6 =====
IF OBJECT_ID('dbo.802_>0_Cantidad_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>0_Cantidad_Ult6](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__0_Cantidad_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>0_Cantidad_UltMes =====
IF OBJECT_ID('dbo.802_>0_Cantidad_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>0_Cantidad_UltMes](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__0_Cantidad_UltMes_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>125_Cantidad_Ult6 =====
IF OBJECT_ID('dbo.802_>125_Cantidad_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>125_Cantidad_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__125_Cantidad_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 802_>125_Cantidad_UltMes =====
IF OBJECT_ID('dbo.802_>125_Cantidad_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[802_>125_Cantidad_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_802__125_Cantidad_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 803_>20_Cantidad_Ult6 =====
IF OBJECT_ID('dbo.803_>20_Cantidad_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[803_>20_Cantidad_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_803__20_Cantidad_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 803_>20_Cantidad_UltMes =====
IF OBJECT_ID('dbo.803_>20_Cantidad_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[803_>20_Cantidad_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_803__20_Cantidad_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>0_Masa_Ult6 =====
IF OBJECT_ID('dbo.804_>0_Masa_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>0_Masa_Ult6](@pMes INT, @pMesIni INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__0_Masa_Ult6_q](@pMes, @pMesIni, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>0_Masa_UltMes =====
IF OBJECT_ID('dbo.804_>0_Masa_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>0_Masa_UltMes](@pMes INT, @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__0_Masa_UltMes_q](@pMes, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>20_Masa_Ult6 =====
IF OBJECT_ID('dbo.804_>20_Masa_Ult6') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>20_Masa_Ult6](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__20_Masa_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 804_>20_Masa_UltMes =====
IF OBJECT_ID('dbo.804_>20_Masa_UltMes') IS NULL EXEC('CREATE FUNCTION [dbo].[804_>20_Masa_UltMes](@pMes INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_804__20_Masa_UltMes_q](@pMes, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 811_AfiliadoCantidad =====
IF OBJECT_ID('dbo.811_AfiliadoCantidad') IS NULL EXEC('CREATE FUNCTION [dbo].[811_AfiliadoCantidad](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX), @pCodEmpresa INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_811_AfiliadoCantidad_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 250_AfiliadoaControlar =====
IF OBJECT_ID('dbo.250_AfiliadoaControlar') IS NULL EXEC('CREATE FUNCTION [dbo].[250_AfiliadoaControlar](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_250_AfiliadoaControlar_q](@pMesFin, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: zConsulta2 =====
IF OBJECT_ID('dbo.zConsulta2') IS NULL EXEC('CREATE FUNCTION [dbo].[zConsulta2](@pMesFin INT, @pMesIni INT, @pSMN INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_zConsulta2_q](@pMesFin, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 300_SubsidioItemCod_Full =====
IF OBJECT_ID('dbo.300_SubsidioItemCod_Full') IS NULL EXEC('CREATE FUNCTION [dbo].[300_SubsidioItemCod_Full](@pFecha DATETIME2(0), @pCI INT)
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_300_SubsidioItemCod_Full_q](@pFecha, @pCI)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_CertificadoEmpresa =====
IF OBJECT_ID('dbo.500_Rpt_CertificadoEmpresa') IS NULL EXEC('CREATE VIEW [dbo].[500_Rpt_CertificadoEmpresa] AS SELECT * FROM [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: xRptEmpresaPromedioNo0 =====
IF OBJECT_ID('dbo.xRptEmpresaPromedioNo0') IS NULL EXEC('CREATE VIEW [dbo].[xRptEmpresaPromedioNo0] AS SELECT * FROM [dbo].[acc_sgpa_xRptEmpresaPromedioNo0_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: zConsulta1 =====
IF OBJECT_ID('dbo.zConsulta1') IS NULL EXEC('CREATE FUNCTION [dbo].[zConsulta1](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_zConsulta1_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rpt_NoPagarMutualista =====
IF OBJECT_ID('dbo.Rpt_NoPagarMutualista') IS NULL EXEC('CREATE FUNCTION [dbo].[Rpt_NoPagarMutualista](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_Rpt_NoPagarMutualista_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== COMPAT OBJECT FOR QUERY: 500_Rpt_NoPagarMutualista =====
IF OBJECT_ID('dbo.500_Rpt_NoPagarMutualista') IS NULL EXEC('CREATE FUNCTION [dbo].[500_Rpt_NoPagarMutualista](@pMes INT, @pMesIni INT, @pSMN NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
(
    SELECT * FROM [dbo].[acc_sgpa_500_Rpt_NoPagarMutualista_q](@pMes, @pMesIni, @pSMN)
)')
GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_AfeccionGrupo_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_AfeccionGrupo_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodAfeccionGrupo) + '') + 1 AS Max
    FROM AfeccionGrupo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_AfeccionTipo_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_AfeccionTipo_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN TRY_CONVERT(float,Max(CodAfeccionTipo) + '') + 1 = 9999 THEN 10000 ELSE TRY_CONVERT(float,Max(CodAfeccionTipo) + '') + 1 END) AS Max
    FROM AfeccionTipo
    WHERE CodAfeccionTipo <> 9999;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Certificacion_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Certificacion_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(NroLlamado) + '') + 1 AS Max
    FROM Certificacion;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Certificador_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Certificador_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodCertificador) + '') + 1 AS Max
    FROM Certificador;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Empresa_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Empresa_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodEmpresa) + '')+1 AS Max
    FROM Empresa
    WHERE (((Empresa.CodEmpresa)<>900));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_FormaPago_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_FormaPago_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodFormaPago) + '') + 1 AS Max
    FROM FormaPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Mutualista_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Mutualista_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodMutualista) + '') + 1 AS Max
    FROM Mutualista;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Patologia_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Patologia_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodPatologia) + '') + 1 AS Max
    FROM Patologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_PrestacionTipo_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_PrestacionTipo_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodPrestacionTipo) + '') + 1 AS Max
    FROM PrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_Recibo_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_Recibo_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(NroRecibo) + '')+1 AS Max
    FROM SubsidioCabezal;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_RegimenJubilatorio_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_RegimenJubilatorio_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodRegimenJubilatorio) + '') + 1 AS Max
    FROM RegimenJubilatorio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_SalidaTipo_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_SalidaTipo_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodSalidaTipo) + '')+1 AS Max
    FROM SalidaTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 001_SituacionPago_Max =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_001_SituacionPago_Max]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Max(CodSituacionPago) + '') + 1 AS Max
    FROM SituacionPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Afiliado_CI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Afiliado_CI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
    FROM Afiliado
    WHERE (((Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_CargadoHL =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_CargadoHL]
    @pCodEmpresa NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio
    FROM Imponible
    WHERE (((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_CargadosHL =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_CargadosHL]
    @pMes TINYINT,
    @pAnio INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI
    FROM Imponible
    WHERE (((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio) AND ((Imponible.CodEmpresa)=@pCodEmpresa))
    GROUP BY Imponible.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Delete_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Delete_Imponible]
    @pCodEmpresa INT,
    @pMes TINYINT,
    @pAno INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Imponible
    WHERE (((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAno));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Delete_NocargadoHL =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Delete_NocargadoHL]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX),
    @pAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM NoCargadoHL
    WHERE (((NoCargadoHL.CodEmpresa)=@pCodEmpresa) AND ((NoCargadoHL.Mes)=@pMes) AND ((NoCargadoHL.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Drop_Bps4Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Drop_Bps4Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DROP TABLE Bps4Tmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_TrabajaActivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_TrabajaActivo]
    @pMes INT,
    @pAno INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.*
    FROM Trabaja
    WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>=TRY_CONVERT(datetime2,'01/' + CONVERT(nvarchar(50), @pMes) + '/' + CONVERT(nvarchar(50), @pAno) )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 101_ImponibleMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_101_ImponibleMes]
    @pMes INT,
    @pAno INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.*
    FROM Imponible
    WHERE (((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAno));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 102_DiasCertificados =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_102_DiasCertificados]
    @pCI INT,
    @pNroLlamado INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(DATEDIFF(day,FechaIni,FechaFin)+1) AS Dias, Count(*) AS Cantidad
    FROM Certificacion
    WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.Efectiva)= 1) AND ((Certificacion.NroLlamado) <= (CASE WHEN @pNroLlamado = 0 THEN Certificacion.NroLlamado ELSE @pNroLlamado END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 102_Prestacion_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_102_Prestacion_Cantidad]
    @pCI INT,
    @pCodPrestacionTipo NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Prestacion
    WHERE (((Prestacion.CI)=@pCI) AND ((Prestacion.CodPrestacionTipo)=@pCodPrestacionTipo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 103_PrestacionesAfiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_103_PrestacionesAfiliado]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CI, Prestacion.Fecha, PrestacionTipo.Descrip AS Tipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta
    FROM Prestacion INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    WHERE (((Prestacion.CI)=@pCI))
    ORDER BY Prestacion.Fecha DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 103_ReintegrosAfiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_103_ReintegrosAfiliado]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ReintegroMutual.CI, ReintegroMutual.Anio + '/' + RIGHT('00' + CONVERT(varchar(2), ReintegroMutual.Mes), 2) AS AnioMes, ReintegroMutual.Fecha, Mutualista.Descrip AS Mutualista, ReintegroMutual.Importe
    FROM ReintegroMutual INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista
    WHERE (((ReintegroMutual.CI)=@pCI))
    ORDER BY ReintegroMutual.Anio + '/' + RIGHT('00' + CONVERT(varchar(2), ReintegroMutual.Mes), 2) DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 110_Borrar_PrimaFallecimiento_CI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_110_Borrar_PrimaFallecimiento_CI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM PrimaFallecimiento
    WHERE (((PrimaFallecimiento.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 110_Imponible_Periodo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_110_Imponible_Periodo]
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(Imponible.Importe) AS Importe
    FROM Imponible
    WHERE (((Imponible.CI)=@pCI) AND ((Imponible.AnioMes) Between @pMesIni And @pMesFin) AND ((Imponible.Concepto)='1'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 110_PrimaFallecimiento_CI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_110_PrimaFallecimiento_CI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PrimaFallecimiento.*
    FROM PrimaFallecimiento
    WHERE (((PrimaFallecimiento.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 145_AfiliadoXCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_145_AfiliadoXCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*
    FROM Afiliado
    WHERE (((Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 150_5_Mejores_Pagos_No_Casmu =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_150_5_Mejores_Pagos_No_Casmu]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5 Afiliado.CI, I.Mes, I.Anio, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres, Empresa.Nombre, I.Importe
    FROM (Imponible AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) INNER JOIN Empresa ON I.CodEmpresa = Empresa.CodEmpresa
    WHERE (((I.CodEmpresa)<>1))
    ORDER BY I.Importe DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 160_Trabaja =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_160_Trabaja]
    @pCI INT,
    @pCodEmpresa INT,
    @pFechaIngreso DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.*
    FROM Trabaja
    WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.CodEmpresa)=@pCodEmpresa) AND ((Trabaja.FechaIngreso)=@pFechaIngreso));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 160_Trabaja_CI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_160_Trabaja_CI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.*
    FROM Trabaja
    WHERE (((Trabaja.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 170_Delete_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_170_Delete_Imponible]
    @pCI INT,
    @pCodEmpresa INT,
    @pFechaIngreso DATETIME2(0),
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pConcepto NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Imponible
    WHERE (((Imponible.CI)=@pCI) AND ((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Fechaingreso)=@pFechaIngreso) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio) AND ((Imponible.Concepto)=@pConcepto));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 170_Insert_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_170_Insert_Imponible]
    @pCI INT,
    @pCodEmpresa INT,
    @pFechaIngreso DATETIME2(0),
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pDiasTrabajados NVARCHAR(MAX),
    @pImporte NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX),
    @pConcepto NVARCHAR(MAX),
    @pIdTrabaja NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Imponible ( CI, CodEmpresa, Fechaingreso, Mes, Anio, Concepto, IdTrabaja, DiasTrabajados, Importe, Usr, Ts, AnioMes )
    SELECT @pCI AS Expr1, @pCodEmpresa AS Expr2, @pFechaIngreso AS Expr3, @pMes AS Expr4, @pAnio AS Expr5, @pConcepto AS Expr6, @pIdTrabaja AS Expr7, @pDiasTrabajados AS Expr8, @pImporte AS Expr9, @pUsr AS Expr10, SYSDATETIME() AS Expr11, TRY_CONVERT(float,CONVERT(nvarchar(max),@pAnio) + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AS Expr12;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 170_Update_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_170_Update_Imponible]
    @pCI INT,
    @pCodEmpresa INT,
    @pFechaIngreso DATETIME2(0),
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pConcepto NVARCHAR(MAX),
    @pDiasTrabajados NVARCHAR(MAX),
    @pImporte NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Imponible SET Imponible.DiasTrabajados = @pDiasTrabajados, Imponible.Importe = @pImporte, Imponible.Usr = @pUsr, Imponible.Ts = SYSDATETIME()
    WHERE (((Imponible.CI)=@pCI) AND ((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Fechaingreso)=@pFechaIngreso) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAnio) AND ((Imponible.Concepto)=@pConcepto));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 180_GrupoEtario_EdadIni =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_180_GrupoEtario_EdadIni]
    @pEdadIni NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT GrupoEtario.*
    FROM GrupoEtario
    WHERE (((GrupoEtario.EdadIni)=@pEdadIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_Delete_600_Rpt_AfiliadoMutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_Delete_600_Rpt_AfiliadoMutualista]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_AfiliadoMutualista];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_Imponible]
    @pMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.*, Trabaja.FechaBaja
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Trabaja.FechaBaja) Is Null)) OR ((((YEAR(DATEADD(month, -1, [Trabaja].[FechaBaja])) * 100 + MONTH(DATEADD(month, -1, [Trabaja].[FechaBaja]))))>@pMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_Delete_600_Rpt_BPS =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_Delete_600_Rpt_BPS]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_BPS];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_ImpRetObrero =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_ImpRetObrero]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
    FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
    WHERE (((SubsidioItem.CodSubsidioItemCod) In (1,2,3,4,5,6,7)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_ImpRetPatronal =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_ImpRetPatronal]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
    FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
    WHERE (((SubsidioItem.CodSubsidioItemCod) In (101, 102)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_ImpRetTotal =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_ImpRetTotal]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
    FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
    WHERE (((SubsidioItem.CodSubsidioItemCod) In (1,2,3,4,5,6,7,101,102)) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_MontoGrabado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_MontoGrabado]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum([SubsidioCabezal].[ImpAguinaldo])+Sum([SubsidioCabezal].[ImpNominal]) AS Importe
    FROM SubsidioCabezal
    WHERE (((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_SubsidioCantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_SubsidioCantidad]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Count(SubsidioCabezal.CI) AS Cantidad
    FROM SubsidioCabezal
    WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_TotImpEmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_TotImpEmp]
    @pMes NVARCHAR(MAX),
    @pAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmpresaPago.Mes, EmpresaPago.Anio, Sum(EmpresaPago.Importe) AS Importe, ((Sum(EmpresaPago.Importe) * 0.5) / 100) AS TributoTotImpMut
    FROM EmpresaPago
    GROUP BY EmpresaPago.Mes, EmpresaPago.Anio
    HAVING (((EmpresaPago.Mes)=@pMes) AND ((EmpresaPago.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_TotTributo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_TotTributo]
    @pMes NVARCHAR(MAX),
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, Sum(SubsidioItem.Importe) AS Importe
    FROM SubsidioCabezal INNER JOIN SubsidioItem ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio
    WHERE (((SubsidioItem.CodSubsidioItemCod)=0 Or (SubsidioItem.CodSubsidioItemCod) = 100) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    GROUP BY SubsidioCabezal.Mes, SubsidioCabezal.Anio
    HAVING (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 220_AfiliadoImponibleMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_220_AfiliadoImponibleMes]
    @pCI INT,
    @pMes INT,
    @pMesIni INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
    WHERE (((Imponible.CI)=@pCI) AND (AnioMes Between @pMesIni And @pMes) AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1'))
    GROUP BY Imponible.Anio, Imponible.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 220_AfiliadoImponibleMes_Old =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_220_AfiliadoImponibleMes_Old]
    @pCI INT,
    @pMes INT,
    @pMesIni INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Imponible.CI)=@pCI) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMes) AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1'))
    GROUP BY Imponible.Anio, Imponible.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_Delete_Receta =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_Delete_Receta]
    @pCI INT,
    @pFecha DATETIME2(0),
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Receta
    WHERE (((Receta.CI)=@pCI) AND ((Receta.Fecha)=@pFecha) AND ((Receta.CodPrestacionTipo)=@pCodPrestacionTipo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_PrestacionAnterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_PrestacionAnterior]
    @pCI INT,
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CI, Max(Prestacion.Fecha) AS Fecha, MIN(0) AS PeriodoRenovacion
    FROM Prestacion INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    WHERE (((Prestacion.CI)=@pCI) AND ((Prestacion.CodPrestacionTipo)=@pCodPrestacionTipo))
    GROUP BY Prestacion.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_PrestacionTipoFromCod =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_PrestacionTipoFromCod]
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PrestacionTipo.*
    FROM PrestacionTipo
    WHERE (((PrestacionTipo.CodPrestacionTipo)=@pCodPrestacionTipo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_Receta =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_Receta]
    @pCI INT,
    @pFecha DATETIME2(0),
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Receta AS R
    WHERE ((([R].[CI])=@pCI) AND (([R].[Fecha])=@pFecha) AND (([R].[CodPrestacionTipo])=@pCodPrestacionTipo));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_Receta_Anterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_Receta_Anterior]
    @pCI INT,
    @pFecha DATETIME2(0),
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Receta AS R
    WHERE R.CI=@pCI AND R.Fecha<@pFecha AND R.CodPrestacionTipo=@pCodPrestacionTipo
    AND EXISTS
    (SELECT 1 FROM Receta AS R2
    WHERE R2.CI=@pCI AND R2.Fecha<@pFecha AND R2.CodPrestacionTipo=@pCodPrestacionTipo
    HAVING MAX(R2.Fecha) = R.Fecha);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 230_Receta_Ultima =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_230_Receta_Ultima]
    @pCI INT,
    @pCodPrestacionTipo INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Receta AS RP
    WHERE RP.CI=@pCI AND RP.CodPrestacionTipo=@pCodPrestacionTipo
    AND EXISTS
    (SELECT 1 FROM Receta AS RP2
    WHERE RP2.CI=@pCI AND RP2.CodPrestacionTipo=@pCodPrestacionTipo
    HAVING MAX(RP2.Fecha) = RP.Fecha);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 240_Grupos_IE =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_240_Grupos_IE]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT InformeEstadistico.Grupo
    FROM InformeEstadistico;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 240_InformeGrupo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_240_InformeGrupo]
    @pGrupo NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT InformeEstadistico.*
    FROM InformeEstadistico
    WHERE (((InformeEstadistico.Grupo)=@pGrupo))
    ORDER BY InformeEstadistico.Orden;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 240_InformeIdRpt =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_240_InformeIdRpt]
    @pIdRpt INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT InformeEstadistico.*
    FROM InformeEstadistico
    WHERE (((InformeEstadistico.IdRpt)=@pIdRpt));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 250_ActivosXEmpresaAUnaFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_250_ActivosXEmpresaAUnaFecha]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.CodEmpresa, Empresa.Nombre, Count(Trabaja.IdTrabaja) AS Cantidad
    FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
    WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>@pFecha) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngreso)<=@pFecha))
    GROUP BY Empresa.CodEmpresa, Empresa.Nombre;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 250_AportantesAUnMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_250_AportantesAUnMes]
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CodEmpresa, Empresa.Nombre, Count(Trabaja.IdTrabaja) AS Cantidad
    FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.IdTrabaja) In (select IdTrabaja From Imponible Where AnioMes = @pAnioMes AND Importe > 0)))
    GROUP BY Trabaja.CodEmpresa, Empresa.Nombre;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_AfiliadoAporteOk =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_AfiliadoAporteOk]
    @pMesIniImp INT,
    @pMesFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Fechaingreso, Imponible.CodEmpresa, Count(Imponible.CI) AS Cantidad
    FROM Imponible
    WHERE (((Imponible.Concepto)='1') AND ((Imponible.Importe)>0) AND ((Imponible.AnioMes)>=@pMesIniImp And (Imponible.AnioMes)<=@pMesFin))
    GROUP BY Imponible.CI, Imponible.Fechaingreso, Imponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_CertificacionCIDia =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_CertificacionCIDia]
    @pCI INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.NroLlamado, Certificacion.CI, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.CI
    FROM Certificacion
    WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.FechaFin)=@pFecha) AND ((Certificacion.Efectiva)= 1));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_CertificacionesAfiliadoMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_CertificacionesAfiliadoMes]
    @pCI INT,
    @pMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.ImporteDeducible, Certificacion.CodSalidaTipo
    FROM Certificacion
    WHERE (((Certificacion.CI)=@pCI) AND ((@pMes) Between (YEAR([FechaIni]) * 100 + MONTH([FechaIni])) And (YEAR([FechaFin]) * 100 + MONTH([FechaFin]))) AND ((Certificacion.Efectiva)= 1))
    ORDER BY Certificacion.FechaIni;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_CertificacionesTmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_CertificacionesTmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CertificacionesTmp.FechaIni, CertificacionesTmp.FechaFin, CertificacionesTmp.ImporteDeducible, CertificacionesTmp.CodSalidaTipo, CertificacionesTmp.CI
    FROM CertificacionesTmp
    ORDER BY CertificacionesTmp.FechaIni;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Delete_CertificacionesTmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Delete_CertificacionesTmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM CertificacionesTmp;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Delete_SubsidioFecha_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Delete_SubsidioFecha_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_SubsidioFecha_Tmp];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_EmpresaxIDSubsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_EmpresaxIDSubsidio]
    @pIDSubsidio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioImponible.CodEmpresa, MIN(Empresa.Nombre) AS DescEmpresa
    FROM SubsidioImponible INNER JOIN Empresa ON SubsidioImponible.CodEmpresa = Empresa.CodEmpresa
    WHERE (((SubsidioImponible.IdSubsidio)=@pIDSubsidio))
    GROUP BY SubsidioImponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Insert_SubsidioFecha_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Insert_SubsidioFecha_Tmp]
    @pIDSubsidio INT,
    @pDesc NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_SubsidioFecha_Tmp] ( IDSubsidio, DescFecha )
    SELECT @pIDSubsidio AS Expr1, @pDesc AS Expr2;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Insert_SubsidioImponibleAnterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Insert_SubsidioImponibleAnterior]
    @pIdSubsidioAnt INT,
    @pIdSubsidio INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SubsidioImponible ( IdSubsidio, Mes, Anio, CodEmpresa, Dias, Importe, Usr, Ts )
    SELECT @pIdSubsidio AS Expr3, SubsidioImponible.Mes, SubsidioImponible.Anio, SubsidioImponible.CodEmpresa, SubsidioImponible.Dias, SubsidioImponible.Importe, @pUsr AS Expr1, SYSDATETIME() AS Expr2
    FROM SubsidioImponible
    WHERE (((SubsidioImponible.IdSubsidio)=@pIdSubsidioAnt));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_JornalAnterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_JornalAnterior]
    @pMes INT,
    @pCi INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.ValorJornal, SubsidioEnfermedad.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio
    FROM (Certificacion AS C INNER JOIN SubsidioEnfermedad ON C.NroLlamado = SubsidioEnfermedad.IdSubsidio) INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
    WHERE (((C.Efectiva)= 1) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C].[FechaIni])) + FORMAT(MONTH([C].[FechaIni]),'00')))<@pMes) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C].[FechaFin])) + FORMAT(MONTH([C].[FechaFin]),'00')))>=@pMes) AND ((C.CI)=@pCi) AND ((SubsidioCabezal.Liquidar)=@pLiquidar)) OR (((C.Efectiva)= 1) AND ((C.CI)=@pCi) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND (EXISTS(SELECT * FROM Certificacion C1 Where C1.Efectiva = 1 And C1.CI = @pCi  And TRY_CONVERT(float,CONVERT(nvarchar(max),YEAR([C1].[FechaIni])) + FORMAT(MONTH([C1].[FechaIni]),'00')) = @pMes And DATEDIFF(day, C.FechaFin, C1.FechaIni) = 1
    )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_JornalAnterior2 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_JornalAnterior2]
    @pCI INT,
    @pLiquidar NVARCHAR(MAX),
    @pMes INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.ValorJornal
    FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
    WHERE ((((YEAR([SubsidioEnfermedad].[FechaIni]) * 100 + MONTH([SubsidioEnfermedad].[FechaIni])))<@pMes) AND (((YEAR([SubsidioEnfermedad].[FechaFin]) * 100 + MONTH([SubsidioEnfermedad].[FechaFin])))>=@pMes) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI)) OR (((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI) AND ((DATEDIFF(day,[SubsidioEnfermedad].[FechaFin],@pFecha))=1))
    ORDER BY SubsidioCabezal.Anio DESC , SubsidioCabezal.Mes DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_JornalAnterior2Ant =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_JornalAnterior2Ant]
    @pCI INT,
    @pLiquidar NVARCHAR(MAX),
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.ValorJornal
    FROM SubsidioCabezal INNER JOIN SubsidioEnfermedad ON SubsidioCabezal.IdSubsidio = SubsidioEnfermedad.IdSubsidio
    WHERE (((SubsidioEnfermedad.FechaIni)=@pFechaIni) AND ((SubsidioEnfermedad.FechaFin)=@pFechaFin) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_RegimenJubilatorioAfiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_RegimenJubilatorioAfiliado]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CodRegimenJubilatorio
    FROM Afiliado
    WHERE (((Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Rpt_PrimaFallecimiento =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Rpt_PrimaFallecimiento]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, YEAR([FechaFallecimiento]) AS Anio, MONTH([FechaFallecimiento]) AS Mes, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, PrimaFallecimiento.Importe AS ImpNominal, 0 AS Importe, PrimaFallecimiento.FechaFallecimiento, TRY_CONVERT(datetime2,'01/' + FORMAT(DATEADD(month,-6,PrimaFallecimiento.FechaFallecimiento),'mm/yyyy')) AS FechaIni, DATEADD(day,-1,TRY_CONVERT(datetime2,'01/' + FORMAT(PrimaFallecimiento.FechaFallecimiento,'mm/yyyy'))) AS FechaFin
    FROM Afiliado INNER JOIN PrimaFallecimiento ON Afiliado.CI = PrimaFallecimiento.CI
    WHERE (((PrimaFallecimiento.FechaFallecimiento) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Rpt_Subsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Rpt_Subsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioItem.CodSubsidioItemCod, SubsidioItem.Importe, SubsidioItemCod.Descrip AS DescSubsidioItemCod, SubsidioItemCod.Tipo, SubsidioItemCod.Signo, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio, SubsidioCabezal.NroRecibo, [600_SubsidioFecha_Tmp].DescFecha, Afiliado.CodBanco, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta
    FROM (((SubsidioCabezal INNER JOIN (SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod) ON SubsidioCabezal.IdSubsidio = SubsidioItem.IdSubsidio) INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco
    WHERE (((SubsidioItemCod.Tipo)='O') AND ((TRY_CONVERT(float,[SubsidioCabezal].[ImpLiquido]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Rpt_SubsidioEmpresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Rpt_SubsidioEmpresa]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezalEmpresa.ImpNominal, SubsidioCabezalEmpresa.ImpAguinaldo, SubsidioCabezalEmpresa.ImpLiquido, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio, SubsidioCabezal.NroRecibo, [600_SubsidioFecha_Tmp].DescFecha, Empresa.Nombre AS DescEmpresa, SubsidioCabezalEmpresa.ImpNominal, Afiliado.CodBanco, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta
    FROM ((((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) INNER JOIN SubsidioCabezalEmpresa ON SubsidioCabezal.IdSubsidio = SubsidioCabezalEmpresa.IdSubsidio) INNER JOIN Empresa ON SubsidioCabezalEmpresa.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco
    WHERE (((TRY_CONVERT(float,[SubsidioCabezal].[ImpLiquido]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioEmpresaAnterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioEmpresaAnterior]
    @pIdSubsidio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezalEmpresa.*
    FROM SubsidioCabezalEmpresa
    WHERE (((SubsidioCabezalEmpresa.IdSubsidio)=@pIdSubsidio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioEmpresaAnteriorVacia =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVacia]
    @pIdSubsidio INT,
    @pLiquidar NVARCHAR(MAX),
    @pCodCasemed NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioImponible.CodEmpresa, Sum(SubsidioImponible.Dias) AS Dias, Sum(SubsidioImponible.Importe) AS Importe, (CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END) AS Promedio
    FROM SubsidioImponible INNER JOIN SubsidioCabezal ON SubsidioImponible.IdSubsidio = SubsidioCabezal.IdSubsidio
    WHERE (((SubsidioImponible.IdSubsidio)=@pIdSubsidio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioImponible.CodEmpresa)<>@pCodCasemed))
    GROUP BY SubsidioImponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioEmpresaAnteriorVaciaCasemed =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioEmpresaAnteriorVaciaCasemed]
    @pIdSubsidio INT,
    @pLiquidar NVARCHAR(MAX),
    @pCodCasemed NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioImponible.CodEmpresa, Sum(SubsidioImponible.Dias) AS Dias, Sum(SubsidioImponible.Importe) AS Importe, Sum(SubsidioImponible.Importe)/180 AS Promedio
    FROM SubsidioImponible INNER JOIN SubsidioCabezal ON SubsidioImponible.IdSubsidio = SubsidioCabezal.IdSubsidio
    WHERE (((SubsidioImponible.IdSubsidio)=@pIdSubsidio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioImponible.CodEmpresa)=@pCodCasemed))
    GROUP BY SubsidioImponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioEnfermedadFromMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioEnfermedadFromMes]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
    FROM SubsidioEnfermedad INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
    WHERE (((SubsidioCabezal.ImpNominal)>0) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar))
    ORDER BY SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIniSubsidio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioFecha]
    @pMes INT,
    @pAnio INT,
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio
    FROM SubsidioEnfermedad INNER JOIN SubsidioCabezal ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio
    WHERE (((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio))
    ORDER BY SubsidioEnfermedad.IdSubsidio, SubsidioEnfermedad.FechaIni;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioFranjaAnt =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioFranjaAnt]
    @pCodSubsidioItemCod INT,
    @pImporte NVARCHAR(MAX),
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (IRPControl.SMNAnt*@pSMN)-(((IRPControl.SMNAnt*@pSMN)*IRPControl.FranjaAnt)/100) AS ImpFrjAnt
    FROM IRPControl
    WHERE ((((IRPControl.SMNAnt*@pSMN)-(((IRPControl.SMNAnt*@pSMN)*IRPControl.FranjaAnt)/100))>@pImporte) AND ((IRPControl.CodIRP)=@pCodSubsidioItemCod));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioImporte =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioImporte]
    @pIdSubsidio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(SubsidioItem.Importe * SubsidioItemCod.Signo) AS Importe
    FROM SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod
    WHERE (((SubsidioItem.IdSubsidio)=@pIdSubsidio) AND ((SubsidioItemCod.Tipo)='O'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioItemCod =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioItemCod]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod.*
    FROM SubsidioItemCod
    WHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioItemId =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioItemId]
    @pCodSubsidioItemCod INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod.*
    FROM SubsidioItemCod
    WHERE (((SubsidioItemCod.CodSubsidioItemCod)=@pCodSubsidioItemCod));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_TrabajaActivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_TrabajaActivo]
    @pMes NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.*
    FROM Trabaja
    WHERE (Trabaja.FechaBaja Is Null OR (YEAR([Trabaja].[FechaBaja]) * 100 + MONTH([Trabaja].[FechaBaja]))>@pMes) And (YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed]))<=@pMes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_Update_ItemIRP =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_Update_ItemIRP]
    @pIDSubsidio INT,
    @pCodSubsidioItemCod INT,
    @pImporte NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SubsidioItem SET SubsidioItem.Importe = SubsidioItem.Importe-@pImporte
    WHERE (((SubsidioItem.CodSubsidioItemCod)=@pCodSubsidioItemCod) AND ((SubsidioItem.IdSubsidio)=@pIDSubsidio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 310_CertificacionAnterior =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_310_CertificacionAnterior]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCI INT,
    @pNroLlamado INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.NroLlamado, Certificacion.NroRecibo, Certificacion.FechaIni, Certificacion.FechaFin
    FROM Certificacion
    WHERE (((Certificacion.NroLlamado)<>@pNroLlamado) AND ((Certificacion.Efectiva)= 1) AND ((Certificacion.CI)=@pCI) AND (([Certificacion].[FechaIni]<=@pFechaFin And [Certificacion].[FechaFin]>=@pFechaIni)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Suma_Importe =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Suma_Importe]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Suma_Puestos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Suma_Puestos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible
    GROUP BY Imponible.CI, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 401_TrabajaActivoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_401_TrabajaActivoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CI
    FROM Trabaja
    WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 403_AportesUlt12xCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_403_AportesUlt12xCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.AnioMes
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Imponible.Concepto)='1') AND ((Imponible.CI)=@pCI) AND ((Imponible.AnioMes)>=(YEAR(DATEADD(month,13,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,13,CAST(GETDATE() AS date))))) AND ((Trabaja.FechaBaja) Is Null))
    GROUP BY Imponible.CI, Imponible.AnioMes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 403_UltProxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_403_UltProxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Max(DATEADD(day,[CertificacionProrroga].[Dias],[CertificacionProrroga].[Fecha])) AS Fecha
    FROM CertificacionProrroga
    WHERE (((CertificacionProrroga.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 450_AfiliadoMutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_450_AfiliadoMutualista]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CodMutualista, Mutualista.Cuota
    FROM Afiliado INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
    WHERE (((Afiliado.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 450_UpdateCuotaMutual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_450_UpdateCuotaMutual]
    @pCodMutualista NVARCHAR(MAX),
    @pImporte NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Mutualista SET Mutualista.Cuota = @pImporte
    WHERE (((Mutualista.CodMutualista)=@pCodMutualista));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_AdPreJubxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_AdPreJubxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AdPreJub.*
    FROM AdPreJub
    WHERE (((AdPreJub.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_AfiliadoCertificadoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_AfiliadoCertificadoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 Certificacion.CI
    FROM Certificacion
    WHERE (((Certificacion.CI)=@pCI) AND ((Certificacion.Efectiva)= 1));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_AfiliadoSubsidioxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_AfiliadoSubsidioxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 SubsidioCabezal.CI
    FROM SubsidioCabezal
    WHERE (((SubsidioCabezal.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_DeleteAdPreJubxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_DeleteAdPreJubxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM AdPreJub
    WHERE (((AdPreJub.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_Imponible]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Imponible.Concepto)='1') AND ((Imponible.AnioMes)>=(YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed]))))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_IMSxAnioMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_IMSxAnioMes]
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IMS.Importe
    FROM IMS
    WHERE (((IMS.AnioMes)=@pAnioMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_IMS_Actual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_IMS_Actual]
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IMS.Importe
    FROM IMS
    WHERE (((IMS.AnioMes)=@pAnioMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_RegimenJubilatorioxCod =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_RegimenJubilatorioxCod]
    @pCodRegimenJubilatorio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RegimenJubilatorio.*
    FROM RegimenJubilatorio
    WHERE (((RegimenJubilatorio.CodRegimenJubilatorio)=@pCodRegimenJubilatorio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_TrabajaActivoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_TrabajaActivoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CodEmpresa
    FROM Trabaja
    WHERE (((Trabaja.CI)=@pCI) AND ((Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_UltSMN =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_UltSMN]
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.Importe, I.AnioMes
    FROM IMS AS I
    WHERE (((I.AnioMes) In (SELECT  MAX(AnioMes) FROM IMS AS I2
    WHERE AnioMes <= @pAnioMes)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_UltSubsidioxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_UltSubsidioxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT S.CI, Sum(S.ValorJornal*30) AS Importe
    FROM SubsidioCabezal AS S
    WHERE (((S.CI)=@pCI) AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[S].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [S].[Mes]), 2)))>=All (SELECT MAX(TRY_CONVERT(float,CONVERT(nvarchar(max),S1.Anio) + RIGHT('00' + CONVERT(varchar(2), S1.Mes), 2))) FROM SubsidioCabezal S1
    WHERE S.CI = S1.CI)))
    GROUP BY S.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 470_AdPreJubPagoxCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_470_AdPreJubPagoxCI]
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AdPreJubPago.*, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM AdPreJubPago INNER JOIN Afiliado ON AdPreJubPago.CI = Afiliado.CI
    WHERE (((AdPreJubPago.CI)=@pCI) AND ((AdPreJubPago.Fecha) Is Null))
    ORDER BY AdPreJubPago.Anio, AdPreJubPago.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 470_AdPreJubPagoxCI-Mes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_470_AdPreJubPagoxCI_Mes]
    @pCI INT,
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AdPreJubPago.*, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM AdPreJubPago INNER JOIN Afiliado ON AdPreJubPago.CI = Afiliado.CI
    WHERE (((AdPreJubPago.Mes)=@pMes) AND ((AdPreJubPago.Anio)=@pAnio) AND ((AdPreJubPago.CI)=@pCI) AND ((AdPreJubPago.Fecha) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 470_DeleteAdPreJubPagoxMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_470_DeleteAdPreJubPagoxMes]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM AdPreJubPago
    WHERE (((AdPreJubPago.Mes)=@pMes) AND ((AdPreJubPago.Anio)=@pAnio) AND ((AdPreJubPago.Fecha) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 470_InsertAdPreJubPagoxMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_470_InsertAdPreJubPagoxMes]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pUsr NVARCHAR(MAX),
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO AdPreJubPago ( CI, Importe, Mes, Anio, Usr, Ts )
    SELECT AdPreJub.CI, (AdPreJub.ImporteMensual * DATEDIFF(day, (CASE WHEN @pFechaIni > AdPreJub.FechaPresentacion THEN @pFechaIni ELSE AdPreJub.FechaPresentacion END), (CASE WHEN @pFechaFin < (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) THEN @pFechaFin ELSE (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) END))  / NULLIF((DATEDIFF(day, @pFechaIni, @pFechaFin) + 1), 0)), @pMes AS Expr1, @pAnio AS Expr2, @pUsr AS Expr3, SYSDATETIME() AS Expr4
    FROM AdPreJub
    WHERE AdPreJub.CI NOT In (SELECT CI FROM AdPreJubPago Where Mes = @pMes And Anio = @pAnio And Fecha Is NOT Null) AND @pFechaIni <= (CASE WHEN AdPreJub.FechaJubilacion IS NULL THEN @pFechaFin ELSE AdPreJub.FechaJubilacion END) AND @pFechaFin >= (CASE WHEN AdPreJub.FechaPresentacion > @pFechaIni THEN AdPreJub.FechaPresentacion ELSE @pFechaIni END);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Delete_Afiliado_Certificado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Delete_Afiliado_Certificado]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Afiliado_Certificado];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_F_Ult_Certif =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_F_Ult_Certif]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.CI, Max(Certificacion.FechaFin) AS F_Ult_Certificacion
    FROM Certificacion
    WHERE (((Certificacion.Efectiva)= 1))
    GROUP BY Certificacion.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_SumaProrrogas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_SumaProrrogas]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CertificacionProrroga.CI, Sum(CertificacionProrroga.Dias) AS Dias
    FROM CertificacionProrroga
    GROUP BY CertificacionProrroga.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Update_Empleos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Update_Empleos]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE A SET A.Empleos = A.Empleos + (CASE WHEN A.Empleos IS NOT NULL THEN ' - ' ELSE '' END) + [Empresa].[Nombre] FROM (Trabaja INNER JOIN [600_Afiliado_Certificado] AS A ON Trabaja.CI = A.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Update_Especialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Update_Especialidad]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE A SET A.Especialidad = A.Especialidad + (CASE WHEN A.Especialidad IS NOT NULL THEN ' - ' ELSE '' END) + [Especialidad].[Descrip] FROM Especialidad INNER JOIN ([600_Afiliado_Certificado] AS A INNER JOIN AfiliadoEspecialidad ON A.CI = AfiliadoEspecialidad.CI) ON Especialidad.CodEspecialidad = AfiliadoEspecialidad.CodEspecialidad
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 490_Delete_Cheque_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_490_Delete_Cheque_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_Cheque_Tmp];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 490_Subsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_490_Subsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM SubsidioCabezal
    WHERE (( [Mes] = 7 And [Anio] = 2007 And [Liquidar] = 1 And CI = 8142038 ));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Delete_Rpt_ResumenSubsidio_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Delete_Rpt_ResumenSubsidio_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_SubsidioResumen_Tmp];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Prorrogas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Prorrogas]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CertificacionProrroga.CI, Sum(CertificacionProrroga.Dias) AS Dias, Max(DATEADD(day, CertificacionProrroga.Dias, CertificacionProrroga.Fecha)) AS Fecha
    FROM CertificacionProrroga
    GROUP BY CertificacionProrroga.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Aporte_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Aporte_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.Mes, Imponible.Anio, RIGHT('00' + CONVERT(varchar(2), Imponible.Mes), 2) + '/' + Imponible.Anio AS MesFormat, Empresa.CodEmpresa, Empresa.Nombre AS DescEmpresa, (CASE WHEN Imponible.Concepto='1' THEN Imponible.Importe ELSE 0 END) AS ImporteAporte, (CASE WHEN Imponible.Concepto='2' THEN Imponible.Importe ELSE 0 END) AS ImporteAguinaldo, (CASE WHEN Imponible.Importe=0 And Imponible.Concepto='1' THEN 1 ELSE NULL END) AS CantCero, Empresa.AporteCasemed, Empresa.AporteAguinaldo, Imponible.Importe, Imponible.CI
    FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa
    WHERE Imponible.CodEmpresa = 900 AND Imponible.Mes = 02 AND Imponible.Anio = 2008;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Cargado_HL_Det =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Cargado_HL_Det]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Sum((CASE WHEN Concepto='1' THEN Imponible.Importe ELSE 0 END)) AS Sueldo, Sum((CASE WHEN Concepto='2' THEN Imponible.Importe ELSE 0 END)) AS Aguinaldo, Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio
    FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Imponible ON (Trabaja.FechaIngreso = Imponible.Fechaingreso) AND (Trabaja.CodEmpresa = Imponible.CodEmpresa) AND (Afiliado.CI = Imponible.CI)
    GROUP BY Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END), Imponible.CodEmpresa, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Certificacion_UltFecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Certificacion_UltFecha]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.CI, Max(Certificacion.FechaFin) AS FechaFin
    FROM Certificacion
    GROUP BY Certificacion.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_DetalleSubsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioImponible.Mes, SubsidioImponible.Anio, SubsidioImponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, SubsidioImponible.Dias, SubsidioImponible.Importe, SubsidioCabezal.Mes AS MesCabezal, SubsidioCabezal.Anio AS AnioCabezal, SubsidioCabezal.Dias AS DiasSubsidio, SubsidioCabezal.Liquidar, SubsidioCabezal.IdSubsidio
    FROM (SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN (SubsidioImponible INNER JOIN Empresa ON SubsidioImponible.CodEmpresa = Empresa.CodEmpresa) ON SubsidioCabezal.IdSubsidio = SubsidioImponible.IdSubsidio
    ORDER BY SubsidioCabezal.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Imponible_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Imponible_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Afiliado.FechaNacimiento
    FROM ((Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI) INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Trabaja.FechaBaja) Is Null) AND ((((TRY_CONVERT(int,[Imponible].[Anio]) * 100) + TRY_CONVERT(int,[Imponible].[Mes]))) Between 200507 And 201404))
     AND Imponible.CI = 11127168;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_NoCargadoHL =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_NoCargadoHL]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + '' <> '' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM Afiliado
    WHERE (((Afiliado.CI) NOT In (Select CI FROM Imponible Where [CodEmpresa]  = 900  And [Mes] = 05 And [Anio] = 2013)  And (Afiliado.CI) In (SELECT CI From Trabaja Where [CodEmpresa] = 900 And ([FechaBaja] Is Null Or (YEAR([FechaBaja]) * 100 + MONTH([FechaBaja])) >= 201305))))
    ORDER BY [CI];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Prestacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Prestacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CI, Prestacion.Fecha, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Prestacion.CodPrestacionTipo, PrestacionTipo.Descrip AS DescPrestacionTipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta, Prestacion.Observaciones, Prestacion.Usr, Prestacion.Ts
    FROM (Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    ORDER BY [Prestacion].[Fecha], [Prestacion].[CI], [Prestacion].[CodPrestacionTipo];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_PrimaFallecimiento_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_PrimaFallecimiento_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, YEAR([FechaFallecimiento]) AS Anio, MONTH([FechaFallecimiento]) AS Mes, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, PrimaFallecimiento.Importe AS ImpNominal, 0 AS Importe, PrimaFallecimiento.FechaFallecimiento, TRY_CONVERT(datetime2,'01/' + FORMAT(DATEADD(month,-6,PrimaFallecimiento.FechaFallecimiento),'mm/yyyy')) AS FechaIni, DATEADD(day,-1,TRY_CONVERT(datetime2,'01/' + FORMAT(PrimaFallecimiento.FechaFallecimiento,'mm/yyyy'))) AS FechaFin
    FROM Afiliado INNER JOIN PrimaFallecimiento ON Afiliado.CI = PrimaFallecimiento.CI
    WHERE (((PrimaFallecimiento.FechaFallecimiento) Is NOT Null)) AND Afiliado.[CI]= 18752605;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_ReintegroMutual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_ReintegroMutual]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ReintegroMutual.CI, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, ReintegroMutual.Mes, ReintegroMutual.Anio, ReintegroMutual.Fecha, ReintegroMutual.CodMutualista, Mutualista.Descrip AS DescMutualista, ReintegroMutual.Importe, ReintegroMutual.Observaciones, ReintegroMutual.Usr, ReintegroMutual.Ts
    FROM (ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI) INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista
    WHERE ( [ReintegroMutual].[Mes] = 7 and [ReintegroMutual].[Anio] = 2003 )
    ORDER BY [ReintegroMutual].[Anio], [ReintegroMutual].[Mes];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_ResumenSubsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_ResumenSubsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.Anio, SubsidioCabezal.Mes, (CASE WHEN LEN(CONVERT(nvarchar(max),SubsidioCabezal.CI))>7 THEN FORMAT(SubsidioCabezal.CI,'@\.@@@\.@@@-@') ELSE FORMAT(SubsidioCabezal.CI,'@@@\.@@@-@') END) AS CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, SubsidioCabezal.Dias, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioCabezal.Liquidar, Afiliado.FechaNacimiento, [600_SubsidioFecha_Tmp].DescFecha, SubsidioCabezal.CI AS CIOrig
    FROM (SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) LEFT JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio
    WHERE (((SubsidioCabezal.ImpLiquido)>0))
    ORDER BY SubsidioCabezal.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Trabaja_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Trabaja_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Trabaja.FechaBaja) Is Null) AND ((Empresa.Ficticia)= 0)) AND Afiliado.[CI]= 36168331;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 505_DeleteImponibleXMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_505_DeleteImponibleXMes]
    @pCodEmpresa INT,
    @pMes INT,
    @pAno INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Imponible
    WHERE (((Imponible.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAno));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Delete_Liquidacion_BPSXEntrega =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Delete_Liquidacion_BPSXEntrega]
    @pNroEntrega INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Liquidacion_BPS
    WHERE (((Liquidacion_BPS.[N_ ENTREGA])=@pNroEntrega));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Insert_LiquidacionBPS =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Insert_LiquidacionBPS]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Liquidacion_BPS ( CI, NOMBRE, APELLIDO, MONTO_TOTAL, MES_DE_CARGO, NOM_EMPRESA, PCT_POR_EMPRESA, FECHA_PER_DESDE, FECHA_PER_HASTA, [N_ ENTREGA], FECHA_DE_ENTREGA, MES, ANIO, LIQUIDO )
    SELECT [506_Rpt_BPS].CI, [506_Rpt_BPS].NOMBRE, [506_Rpt_BPS].APELLIDO, [506_Rpt_BPS].[MONTO TOTAL], [506_Rpt_BPS].[MES DE CARGO], [506_Rpt_BPS].NOM_EMPRESA, [506_Rpt_BPS].[% POR EMPRESA], [506_Rpt_BPS].FECHA_PER_DESDE, [506_Rpt_BPS].FECHA_PER_HASTA, [506_Rpt_BPS].[Nº ENTREGA], [506_Rpt_BPS].[FECHA DE ENTREGA], [506_Rpt_BPS].[MES DE CARGO] % 100 AS Expr1, ([506_Rpt_BPS].[MES DE CARGO]/100) AS Expr2, [506_Rpt_BPS].[LiQUIDO BPS]
    FROM [acc_sgpa_506_Rpt_BPS_q] AS [506_Rpt_BPS]
    WHERE ((([506_Rpt_BPS].CI) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Rpt_LiquidacionBPS =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Rpt_LiquidacionBPS]
    @pMes INT,
    @pAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Liquidacion_BPS.*
    FROM Liquidacion_BPS
    WHERE (((Liquidacion_BPS.MES)=@pMes) AND ((Liquidacion_BPS.ANIO)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Rpt_Subsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Rpt_Subsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, SubsidioCabezal.Dias, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, SubsidioCabezal_BPS.IdSubsidio, SubsidioCabezal.NroRecibo, SubsidioEnfermedad.FechaIni, SubsidioEnfermedad.FechaFin, SubsidioEnfermedad.FechaIniSubsidio, SubsidioEnfermedad.FechaFinSubsidio, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, (CASE WHEN [SubsidioCabezal].Dias>0 THEN [SubsidioCabezal].[ImpNominal]/SubsidioCabezal.Dias*0.7 ELSE 0 END) AS Jornal70, (CASE WHEN [SubsidioCabezal].Dias>0 THEN SubsidioCabezal.ImpAguinaldo/SubsidioCabezal.Dias*0.7 ELSE 0 END) AS Aguinaldo70, SubsidioCabezal_BPS.DiasBPS, SubsidioCabezal_BPS.LiquidoBPS, SubsidioCabezal_BPS.LiquidoPagar, Banco.Descripcion AS Banco, Afiliado.NroCuenta, SubsidioCabezal.Mes, SubsidioCabezal.Anio, Afiliado.EMail
    FROM (SubsidioEnfermedad INNER JOIN ((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) LEFT JOIN SubsidioCabezal_BPS ON SubsidioCabezal.IdSubsidio = SubsidioCabezal_BPS.IdSubsidio) ON SubsidioEnfermedad.IdSubsidio = SubsidioCabezal.IdSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_TotalLiquidoBPSCIMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_TotalLiquidoBPSCIMes]
    @pCI INT,
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum(SubsidioCabezal_BPS.LiquidoBPS) AS LiquidoBPS
    FROM SubsidioCabezal INNER JOIN SubsidioCabezal_BPS ON SubsidioCabezal.IdSubsidio = SubsidioCabezal_BPS.IdSubsidio
    WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Update_Liquidos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Update_Liquidos]
AS
BEGIN
    SET NOCOUNT ON;
    IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','LiquidoPagar') IS NOT NULL
    BEGIN
        IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','IdSubsidio') IS NOT NULL
            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.IdSubsidio = sc.IdSubsidio');
        ELSE IF COL_LENGTH('dbo.506_Rpt_Liquidos_Subsidios','Id') IS NOT NULL
            EXEC (N'UPDATE sc SET sc.ImpLiquido = r.LiquidoPagar FROM SubsidioCabezal sc INNER JOIN [506_Rpt_Liquidos_Subsidios] r ON r.Id = sc.IdSubsidio');
        ELSE
            SELECT 1 AS NoOp
    END
    ELSE
    BEGIN
        SELECT 1 AS NoOp
    END
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 601_Rpt_Recibo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_601_Rpt_Recibo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [600_Rpt_Recibo].*, SubsidioCabezal.FechaPago
    FROM [600_Rpt_Recibo] INNER JOIN SubsidioCabezal ON [600_Rpt_Recibo].IdSubsidio = SubsidioCabezal.IdSubsidio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 765_CertificacionContinua =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_765_CertificacionContinua]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Certificacion.CI
    FROM Certificacion
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaIni)<=@pFecha) AND ((Certificacion.FechaFin)>@pFecha));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 765_CertificacionEmpalma =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_765_CertificacionEmpalma]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Certificacion.CI
    FROM Certificacion
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaIni)=DATEADD(day, 1, @pFecha)))
    and Certificacion.CI In (select CI from certificacion where efectiva= 1 and FechaFin=@pFecha);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 790_Delete_Rpt_Recibo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_790_Delete_Rpt_Recibo]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_Recibo];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_AfiliadoImponible_Mes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_AfiliadoImponible_Mes]
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Imponible.Concepto)='1') AND ((Trabaja.FechaBaja) Is Null) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_AfiliadoImponible_Mes_Fecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Fecha]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.Fechaingreso = Trabaja.FechaIngreso) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.CI = Trabaja.CI)
    WHERE (((Trabaja.FechaBaja) Is Null) AND ((Imponible.Concepto)='1') AND (((YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed])))<=@pMes) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END))) OR (((Imponible.Concepto)='1') AND (((YEAR([Trabaja].[FechaIngCasemed]) * 100 + MONTH([Trabaja].[FechaIngCasemed])))<=@pMes) AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)) AND (((YEAR([Trabaja].[FechaBaja]) * 100 + MONTH([Trabaja].[FechaBaja])))>@pMes))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_AfiliadoImponible_Mes_Todos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_AfiliadoImponible_Mes_Todos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible
    WHERE (((Imponible.Concepto)='1'))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Afiliado_Imponible_Mes_Fecha =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Afiliado_Imponible_Mes_Fecha]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe
    FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
    WHERE (((Imponible.Concepto)='1') AND (((Trabaja.FechaBaja) Is Null) Or (YEAR(Trabaja.FechaBaja) * 100 + MONTH(Trabaja.FechaBaja)) > @pMes)
    AND ((Imponible.CodEmpresa)=(CASE WHEN @pCodEmpresa=0 THEN [Imponible].[CodEmpresa] ELSE @pCodEmpresa END)))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Borrar_Rpt_CantidadDescrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Borrar_Rpt_CantidadDescrip]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_CantidadDescrip];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Borrar_Rpt_CantidadDescrip2 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Borrar_Rpt_CantidadDescrip2]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_CantidadDescrip2];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Cantidad_Empresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Cantidad_Empresa]
    @pCodEmpresa INT,
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Trabaja.CodEmpresa)=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Cantidad_Otras =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Cantidad_Otras]
    @pCodEmpresa INT,
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.CodEmpresa)<>(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 800_Insert_Cantidad_Empresa_Todas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_800_Insert_Cantidad_Empresa_Todas]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip )
    SELECT Count(*) AS Cantidad, MIN(Empresa.Nombre) AS FirstOfNombre
    FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)))
    GROUP BY Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_Activos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_Activos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (Afiliado.CI) In (Select CI From Trabaja Where FechaBaja is Null);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_CertificacionesxAnio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_CertificacionesxAno]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT YEAR(Certificacion.FechaCertificacion) AS Anio, Count(*) AS Cantidad
    FROM Certificacion
    WHERE (((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((Certificacion.Efectiva)= 1))
    GROUP BY YEAR(Certificacion.FechaCertificacion);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_CertificadosActivos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_CertificadosActivos]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)
    )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_CertificadosActivos_Original =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_CertificadosActivos_Original]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) And (Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)
    )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_Certificados_AnioCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_Certificados_AnoCI]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Certificacion.CI, YEAR(Certificacion.FechaCertificacion) AS Anio
    FROM Certificacion
    WHERE (((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((Certificacion.Efectiva)= 1));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_Insert_CertificacionesxAnio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_Insert_CertificacionesxAno]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Cantidad, Descrip )
    SELECT [805_CertificacionesxAnio].Anio, [805_CertificacionesxAnio].Cantidad, [805_CertificacionesxAnio].Anio
    FROM [805_CertificacionesxAnio](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 805_Insert_CertificadosxAnio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_805_Insert_CertificadosxAno]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Descrip, Cantidad, Codigo )
    SELECT [805_Certificados_AnioCI].Anio, Count(*) AS Expr1, [805_Certificados_AnioCI].Anio
    FROM [805_Certificados_AnioCI](NULL, NULL)
    GROUP BY [805_Certificados_AnioCI].Anio
    ORDER BY [805_Certificados_AnioCI].Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_CertificadosEntre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_CertificadosEntre]
    @pAnioIni NVARCHAR(MAX),
    @pAnioFin NVARCHAR(MAX),
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_CertificadosMayores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_CertificadosMayores]
    @pAnioIni NVARCHAR(MAX),
    @pFechaIni NVARCHAR(MAX),
    @pFechaFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_CertificadosMenores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_CertificadosMenores]
    @pAnioIni NVARCHAR(MAX),
    @pFechaIni NVARCHAR(MAX),
    @pFechaFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_CertificadosSexo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_CertificadosSexo]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad, Afiliado.Sexo
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))))
    GROUP BY Afiliado.Sexo
    HAVING (((Afiliado.Sexo) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 807_CertificadosEspecialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_807_CertificadosEspecialidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
    WHERE (((Afiliado.CI) In (Select Ci From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))))
    GROUP BY AfiliadoEspecialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 808_CertificadosAfecciones =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_808_CertificadosAfecciones]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)= (CASE WHEN @pCodPatologia is NOT Null THEN @pCodPatologia ELSE AfeccionGrupo.CodPatologia END)  ))
    GROUP BY Certificacion.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 808_Certificados_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_808_Certificados_Cantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Certificacion
    WHERE (((Certificacion.Efectiva)= 1) AND FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 809_AfiliadoActivoFecha_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_809_AfiliadoActivoFecha_Cantidad]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaIngCasemed <= @pFecha And
    ((FechaBaja Is Null Or FechaBaja > @pFecha) AND
    FechaIngreso <= @pFecha))));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 809_AfiliadoActivo_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_809_AfiliadoActivo_Cantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja where FechaIngCasemed<= CAST(GETDATE() AS date) And (fechabaja is null Or FechaBaja > CAST(GETDATE() AS date)))));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 809_AfiliadoFecha_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_809_AfiliadoFecha_Cantidad]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (SELECT CI FROM TRABAJA WHERE FechaIngreso <= @pFecha)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 809_Afiliado_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_809_Afiliado_Cantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadoActivoEntre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadoActivoEntre]
    @pAnioIni NVARCHAR(MAX),
    @pAnioFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadoActivoMayores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadoActivoMayores]
    @pAnioIni NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadoActivoMenores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadoActivoMenores]
    @pAnioIni NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosActivoSexo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosActivoSexo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.Sexo, Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI FROM Trabaja WHERE FechaBaja is Null)))
    GROUP BY Afiliado.Sexo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosEntre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosEntre]
    @pAnioIni NVARCHAR(MAX),
    @pAnioFin NVARCHAR(MAX),
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha)) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date))) Between @pAnioIni And @pAnioFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosMayores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosMayores]
    @pAnioIni NVARCHAR(MAX),
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha) AND (DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))>=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosMenores =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosMenores]
    @pAnioIni NVARCHAR(MAX),
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngreso <= @pFecha)) AND ((DATEDIFF(year,[Afiliado].[FechaNacimiento],CAST(GETDATE() AS date)))<=@pAnioIni));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosSexo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosSexo]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.Sexo, Count(*) AS Cantidad
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI FROM Trabaja WHERE (FechaBaja is Null Or FechaBaja > @pFecha) And FechaIngCasemed <= @pFecha)))
    GROUP BY Afiliado.Sexo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_Delete_Rpt_<125_Pct =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_Delete_Rpt__125_Pct]
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [600_Rpt_<125_Pct];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 812_AfiliadoActivoEspecialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_812_AfiliadoActivoEspecialidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where FechaBaja Is Null)))
    GROUP BY AfiliadoEspecialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 812_AfiliadosEspecialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_812_AfiliadosEspecialidad]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.CodEspecialidad AS Codigo, MIN(Especialidad.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM (Afiliado INNER JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad
    WHERE (((Afiliado.CI) In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > @pFecha) And FechaIngCasemed <= @pFecha)))
    GROUP BY AfiliadoEspecialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 813_CertificacionAfeccionDistintas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_813_CertificacionAfeccionDistintas]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Certificacion.CI, Certificacion.CodAfeccionTipo
    FROM Certificacion
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 814_Update_Especialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_814_Update_Especialidad]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [600_Rpt_<125_Pct] SET [600_Rpt_<125_Pct].Especialidad = [600_Rpt_<125_Pct].Especialidad + (CASE WHEN [600_Rpt_<125_Pct].Especialidad + '' <> '' THEN ' - ' ELSE '' END) + acc_sgpa_Rs_AfiliadoEspecialidadDesc_q.Descrip FROM [acc_sgpa_Rs_AfiliadoEspecialidadDesc_q] INNER JOIN [600_Rpt_<125_Pct] ON acc_sgpa_Rs_AfiliadoEspecialidadDesc_q.CI = [600_Rpt_<125_Pct].CI
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 816_Certificados_GrupoAfeccion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_816_Certificados_GrupoAfeccion]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionGrupo.CodAfeccionGrupo AS Codigo, MIN(AfeccionGrupo.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
    GROUP BY AfeccionGrupo.CodAfeccionGrupo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 817_Certificados_Patologia =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_817_Certificados_Patologia]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descripcion, Count(*) AS Cantidad
    FROM ((AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
    GROUP BY Patologia.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 818_Certificados_Patologia =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_818_Certificados_Patologia]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Patologia.CodPatologia AS Codigo, MIN(Patologia.Descrip) AS Descripcion, Sum(DATEDIFF(day, Certificacion.FechaIni, Certificacion.FechaFin) + 1) AS Cantidad
    FROM ((AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
    GROUP BY Patologia.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 819_Certificados_AfeccionGrupo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_819_Certificados_AfeccionGrupo]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionGrupo.CodAfeccionGrupo AS Codigo, MIN(AfeccionGrupo.Descrip) AS Descrip, Sum(DATEDIFF(day,Certificacion.FechaIni,Certificacion.FechaFin)+1) AS Cantidad
    FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
    GROUP BY AfeccionGrupo.CodAfeccionGrupo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 820_Certificados_AfeccionTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_820_Certificados_AfeccionTipo]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionTipo.CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descrip, Sum(DATEDIFF(day,Certificacion.FechaIni,Certificacion.FechaFin)+1) AS Cantidad
    FROM (AfeccionTipo INNER JOIN (Afiliado INNER JOIN Certificacion ON Afiliado.CI = Certificacion.CI) ON AfeccionTipo.CodAfeccionTipo = Certificacion.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND (([AfeccionGrupo].[CodPatologia])=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
    GROUP BY AfeccionTipo.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 821_Insert_SexoPatologia_Cantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_821_Insert_SexoPatologia_Cantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip2] ( Descrip, Descrip2, Cantidad )
    SELECT Afiliado.Sexo, MIN(Patologia.Descrip) AS FirstOfDescrip, Count(*) AS Expr1
    FROM (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
    GROUP BY Afiliado.Sexo, AfeccionGrupo.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 821_Insert_SexoPatologia_Dias =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_821_Insert_SexoPatologia_Dias]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip2] ( Descrip, Descrip2, Cantidad )
    SELECT Afiliado.Sexo, MIN(Patologia.Descrip) AS FirstOfDescrip, Sum(DATEDIFF(day, FechaIni, FechaFin)+1) AS Expr1
    FROM (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)))
    GROUP BY Afiliado.Sexo, AfeccionGrupo.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 822_AfiliadoGE =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_822_AfiliadoGE]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END) AS FechaHoy, DATEDIFF(year,FechaNacimiento,CAST(GETDATE() AS date))-(CASE WHEN DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END)<=CAST(GETDATE() AS date) THEN 0 ELSE 1 END) AS Edad, Afiliado.FechaNacimiento, Afiliado.Sexo
    FROM Afiliado
    WHERE (((Afiliado.FechaNacimiento) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 824_PrestacionesCantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_824_PrestacionesCantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Count(*) AS Cantidad
    FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)))
    GROUP BY Prestacion.CodPrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 825_PrestacionesImporte_Pesos =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_825_PrestacionesImporte_Pesos]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Sum(Prestacion.Importe) AS Importe
    FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)) AND ((Prestacion.Moneda)='$'))
    GROUP BY Prestacion.CodPrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 826_PrestacionesImporte_Dolares =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_826_PrestacionesImporte_Dolares]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CodPrestacionTipo, MIN(PrestacionTipo.Descrip) AS DescPrestacionTipo, Sum(Prestacion.Importe) AS Importe
    FROM (Afiliado INNER JOIN Prestacion ON Afiliado.CI = Prestacion.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo
    WHERE (((Prestacion.Fecha) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [Fecha] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [Fecha] END)) AND ((Prestacion.Moneda)='U$S'))
    GROUP BY Prestacion.CodPrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 828_Cantidad_Empresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_828_Cantidad_Empresa]
    @pCodEmpresa NVARCHAR(MAX),
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Trabaja.CodEmpresa)=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND (EXISTS(SELECT * FROM Imponible Where Trabaja.CI = Imponible.CI And Trabaja.FechaIngreso = Imponible.FechaIngreso And Imponible.CodEmpresa = Trabaja.CodEmpresa AND Concepto = '1' AND AnioMes = (CASE WHEN @pFecha IS NULL THEN (YEAR(DATEADD(month, -2, CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))) ELSE (YEAR(DATEADD(month, -2, @pFecha)) * 100 + MONTH(DATEADD(month, -2, @pFecha))) END) And Importe > 0
      )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 828_Cantidad_Otras =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_828_Cantidad_Otras]
    @pCodEmpresa INT,
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM Trabaja INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.CodEmpresa)<>(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE [Trabaja].[CodEmpresa] END)) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND (EXISTS(SELECT * FROM Imponible Where Trabaja.CI = Imponible.CI And Trabaja.FechaIngreso = Imponible.FechaIngreso And Imponible.CodEmpresa = Trabaja.CodEmpresa AND Concepto = '1' AND AnioMes = (CASE WHEN @pFecha IS NULL THEN (YEAR(DATEADD(month, -2, CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))) ELSE (YEAR(DATEADD(month, -2, @pFecha)) * 100 + MONTH(DATEADD(month, -2, @pFecha))) END) And Importe > 0
      )));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 828_Insert_Cantidad_Empresa_Todas =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_828_Insert_Cantidad_Empresa_Todas]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip )
    SELECT Count(*) AS Cantidad, MIN(Empresa.Nombre) AS FirstOfNombre
    FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa
    WHERE (((Empresa.Ficticia)= 0) AND ((Trabaja.FechaIngCasemed)<=(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND ((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>(CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END)) AND (EXISTS (SELECT * FROM Imponible Where Trabaja.CI = Imponible.CI And Trabaja.FechaIngreso = Imponible.FechaIngreso And Imponible.CodEmpresa = Trabaja.CodEmpresa AND Concepto = '1' AND AnioMes = (CASE WHEN @pFecha IS NULL THEN (YEAR(DATEADD(month, -2, CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))) ELSE (YEAR(DATEADD(month, -2, @pFecha)) * 100 + MONTH(DATEADD(month, -2, @pFecha))) END) And Importe > 0
      )))
    GROUP BY Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 830_CantidadPorPuesto =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_830_CantidadPorPuesto]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CodEmpresa, Count(Trabaja.CI) AS Cantidad
    FROM Trabaja
    WHERE (((Trabaja.FechaBaja) Is Null Or (Trabaja.FechaBaja)>@pFecha))
    GROUP BY Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 830_CantidadPorPuestoNo0 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_830_CantidadPorPuestoNo0]
    @pFecha DATETIME2(0),
    @pMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.CodEmpresa, Count(t.CI) AS Cantidad
    FROM Trabaja AS t
    WHERE (((t.FechaBaja) Is Null Or (t.FechaBaja)>@pFecha)) AND EXISTS (SELECT 1 FROM Imponible i WHERE t.CI=i.CI and i.CodEmpresa = t.CodEmpresa AND i.AnioMes = @pMes AND i.Importe>0 And i.Concepto='1')
    GROUP BY t.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 999_Excel_Tmp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_999_Excel_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Rpt_Historia_Vandalismo_S;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 999_Parametros =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_999_Parametros]
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
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_999_Parametros_Delete]
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
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_999_Parametros_Insert]
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
    SELECT @pLogin AS Expr1, @pClave AS Expr2, @pOrden AS Expr3, @pValue1 AS Expr4, @pValue2 AS Expr5, @pValue3 AS Expr6, @pValue4 AS Expr7, @pValue5 AS Expr8;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: BpsMutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_BpsMutualista]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BpsFormat.Mutualista
    FROM BpsFormat
    GROUP BY BpsFormat.Mutualista;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Buscar duplicados por Bps4 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Buscar_duplicados_por_Bps4]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT [CI], [TipoReg], [AcumulacionLaboral], [Concepto], [Imponible]
    FROM Bps4
    WHERE [CI] In (SELECT [CI] FROM [Bps4] As Tmp GROUP BY [CI] HAVING Count(*)>1 )
    ORDER BY [CI];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Buscar duplicados por zRs_AEsp =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Buscar_duplicados_por_zRs_AEsp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT [CI], [EspNom1], [EspNom2], [EspNom3]
    FROM zRs_AEsp
    WHERE [CI] In (SELECT [CI] FROM [zRs_AEsp] As Tmp GROUP BY [CI] HAVING Count(*)>1 )
    ORDER BY [CI];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Afiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Afiliado]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Mutualista.Descrip AS DescMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, RegimenJubilatorio.Descrip AS DescRegimenJubilatorio, Afiliado.Usr, Afiliado.Ts, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Mutualista.Cuota, Afiliado.Direccion, Afiliado.PagaMutualista, Afiliado.CodDepartamento, Departamento.Descrip AS DescDepartamento, Afiliado.Movil
    FROM ((Afiliado INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista) INNER JOIN RegimenJubilatorio ON Afiliado.CodRegimenJubilatorio = RegimenJubilatorio.CodRegimenJubilatorio) LEFT JOIN Departamento ON Afiliado.CodDepartamento = Departamento.CodDepartamento;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Aporte =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Aporte]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.Mes, Imponible.Anio, RIGHT('00' + CONVERT(varchar(2), Imponible.Mes), 2) + '/' + Imponible.Anio AS MesFormat, Empresa.CodEmpresa, Empresa.Nombre AS DescEmpresa, (CASE WHEN Imponible.Concepto='1' THEN Imponible.Importe ELSE 0 END) AS ImporteAporte, (CASE WHEN Imponible.Concepto='2' THEN Imponible.Importe ELSE 0 END) AS ImporteAguinaldo, (CASE WHEN Imponible.Importe=0 And Imponible.Concepto='1' THEN 1 ELSE NULL END) AS CantCero, Empresa.AporteCasemed, Empresa.AporteAguinaldo, Imponible.Importe, Imponible.CI
    FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Certificacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Certificacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.NroLlamado, Certificacion.CI, Certificacion.NroRecibo, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.CodAfeccionTipo, AfeccionTipo.Descrip AS DescAfeccionTipo, Certificacion.CodCertificador, Certificador.Descrip AS DescCertificador, Certificacion.CodSalidaTipo, SalidaTipo.Descrip AS DescSalidaTipo, Certificacion.Efectiva, Certificacion.Indicaciones, Certificacion.ImporteDeducible, Certificacion.Usr, Certificacion.Ts, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado
    FROM (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Certificacion2 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Certificacion2]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.NroLlamado, (CASE WHEN LEN(Certificacion.CI)>=8 THEN FORMAT(Certificacion.CI, '@.@@@.@@@-@') ELSE FORMAT(Certificacion.CI, '@@@.@@@-@') END) AS CI, Certificacion.NroRecibo, Certificacion.FechaRecibido, Certificacion.FechaCertificacion, Certificacion.FechaIni, Certificacion.FechaFin, Certificacion.CodAfeccionTipo, AfeccionTipo.Descrip AS DescAfeccionTipo, Certificacion.CodCertificador, Certificador.Descrip AS DescCertificador, Certificacion.CodSalidaTipo, Certificacion.Efectiva, Certificacion.Indicaciones, Certificacion.ImporteDeducible, Certificacion.Usr, Certificacion.Ts, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado
    FROM ((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Discount =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Discount]
    @pCodDiscount NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pFecha DATETIME2(0),
    @pCodBanco NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.NroFunCuenta, Afiliado.NroCuenta, @pFecha AS Fecha, SubsidioCabezal.ImpLiquido
    FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
    WHERE (((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((Afiliado.CodBanco)=@pCodBanco));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Imponible]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe
    FROM (Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Imponible_Activo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Imponible_Activo]
    @pMesIni NVARCHAR(MAX),
    @pMesFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, (CASE WHEN LEN(Imponible.CI)>=8 THEN FORMAT(Imponible.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Imponible.CI,'@@@\.@@@-@') END) AS CIFormat, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Mes, Imponible.Anio, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Afiliado.FechaNacimiento
    FROM ((Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON Imponible.CI = Afiliado.CI) INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
    WHERE (((Trabaja.FechaBaja) Is Null) AND ((((TRY_CONVERT(int,[Imponible].[Anio]) * 100) + TRY_CONVERT(int,[Imponible].[Mes]))) Between @pMesIni And @pMesFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Mutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Mutualista]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Mutualista.CodMutualista, Mutualista.Descrip, Mutualista.Direccion, Mutualista.Telefono, Mutualista.Fax, Mutualista.EMail, Mutualista.DiaPago, Mutualista.CodFormaPago, FormaPago.Descrip AS DescFormaPago, Mutualista.PersonaContacto, Mutualista.Cuota, Mutualista.Usr, Mutualista.Ts
    FROM Mutualista INNER JOIN FormaPago ON Mutualista.CodFormaPago = FormaPago.CodFormaPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Prestacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Prestacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.CI, Prestacion.Fecha, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, Prestacion.CodPrestacionTipo, PrestacionTipo.Descrip AS DescPrestacionTipo, Prestacion.Moneda, Prestacion.Importe, Prestacion.Boleta, Prestacion.Observaciones, Prestacion.Usr, Prestacion.Ts
    FROM (Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI) INNER JOIN PrestacionTipo ON Prestacion.CodPrestacionTipo = PrestacionTipo.CodPrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_ReintegroMutual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_ReintegroMutual]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ReintegroMutual.CI, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, ReintegroMutual.Mes, ReintegroMutual.Anio, ReintegroMutual.Fecha, ReintegroMutual.CodMutualista, Mutualista.Descrip AS DescMutualista, ReintegroMutual.Importe, ReintegroMutual.Observaciones, ReintegroMutual.Usr, ReintegroMutual.Ts
    FROM (ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI) INNER JOIN Mutualista ON ReintegroMutual.CodMutualista = Mutualista.CodMutualista;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_SubsidioCabezal =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_SubsidioCabezal]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.IdSubsidio, SubsidioCabezal.Mes, SubsidioCabezal.Anio, SubsidioCabezal.CI, SubsidioCabezal.Liquidar, SubsidioCabezal.ValorJornal, SubsidioCabezal.Dias, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, SubsidioCabezal.ImpLiquido, SubsidioCabezal.NroRecibo, SubsidioCabezal.FechaPago, SubsidioCabezal.Usr, SubsidioCabezal.Ts, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, [600_SubsidioFecha_Tmp].DescFecha, Banco.Descripcion AS DescBanco, Afiliado.NroCuenta, Afiliado.NroFunCuenta, Afiliado.CodBanco
    FROM ((SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI) INNER JOIN [600_SubsidioFecha_Tmp] ON SubsidioCabezal.IdSubsidio = [600_SubsidioFecha_Tmp].IDSubsidio) LEFT JOIN Banco ON Afiliado.CodBanco = Banco.CodBanco;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Trabaja =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Trabaja]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa
    FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Trabaja.FechaBaja) Is Null) AND ((Empresa.Ficticia)= 0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Trabaja_Rpt =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Trabaja_Rpt]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, Afiliado.CI, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, Trabaja.FechaBaja
    FROM (Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: rsAfiliadoActivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_rsAfiliadoActivo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*
    FROM Afiliado
    WHERE (((Afiliado.CI) In (select ci from trabaja where fechabaja is null)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfeccionGrupo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfeccionGrupo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionGrupo.*
    FROM AfeccionGrupo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfeccionGrupo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfeccionGrupo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionGrupo.CodAfeccionGrupo, AfeccionGrupo.Descrip
    FROM AfeccionGrupo
    ORDER BY AfeccionGrupo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfeccionTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfeccionTipo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionTipo.*
    FROM AfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfeccionTipo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfeccionTipo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfeccionTipo.CodAfeccionTipo, AfeccionTipo.Descrip
    FROM AfeccionTipo
    ORDER BY AfeccionTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Afiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Afiliado]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*
    FROM Afiliado;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoApunte =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoApunte]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoApunte.CI, AfiliadoApunte.Fecha, AfiliadoApunte.Descrip, AfiliadoApunte.Usr, AfiliadoApunte.Ts
    FROM AfiliadoApunte
    ORDER BY AfiliadoApunte.Fecha DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoApunteFromPeriodo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoApunteFromPeriodo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT afiliado.ci, nombres, apellido1, apellido2, fecha, descrip
    FROM afiliado INNER JOIN afiliadoapunte ON afiliado.ci=afiliadoapunte.ci;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoCristalin =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoCristalin]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*
    FROM Afiliado
    WHERE (((Afiliado.CI) In (Select CI From Trabaja where CodEmpresa=7)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoEspecialidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.*
    FROM AfiliadoEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoEspecialidadDesc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoEspecialidadDesc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.CI, Especialidad.Descrip
    FROM AfiliadoEspecialidad INNER JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoEspecialidad_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoEspecialidad_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AfiliadoEspecialidad.CI, AfiliadoEspecialidad.CodEspecialidad
    FROM AfiliadoEspecialidad
    ORDER BY AfiliadoEspecialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoImponibleMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoImponibleMes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.Mes, Imponible.Anio, Sum(Imponible.Importe) AS Importe, MIN(Imponible.AnioMes) AS AnioMes
    FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
    WHERE (((Imponible.Concepto)='1') AND ((Trabaja.FechaBaja) Is Null))
    GROUP BY Imponible.CI, Imponible.Mes, Imponible.Anio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AfiliadoImponibleMesNoBaja =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AfiliadoImponibleMesNoBaja]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.AnioMes, Sum(Imponible.Importe) AS Importe, MIN(Imponible.AnioMes) AS FirstOfAnioMes
    FROM Imponible INNER JOIN Trabaja ON (Imponible.CI = Trabaja.CI) AND (Imponible.CodEmpresa = Trabaja.CodEmpresa) AND (Imponible.Fechaingreso = Trabaja.FechaIngreso)
    WHERE (((Imponible.Concepto)='1'))
    GROUP BY Imponible.CI, Imponible.AnioMes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Afiliado_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Afiliado_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, [Nombres] + ' ' + [Apellido1] + ' ' + [Apellido2] AS Descrip
    FROM Afiliado;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Afiliado_FechaNacimiento =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Afiliado_FechaNacimiento]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*
    FROM Afiliado
    WHERE (((Afiliado.FechaNacimiento) Is NOT Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AporteTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AporteTipo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AporteTipo.*
    FROM AporteTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_AporteTipo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_AporteTipo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AporteTipo.CodAporteTipo, AporteTipo.Descrip
    FROM AporteTipo
    ORDER BY AporteTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_BajaMotivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_BajaMotivo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BajaMotivo.*
    FROM BajaMotivo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_BajaMotivo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_BajaMotivo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BajaMotivo.CodBajaMotivo, BajaMotivo.Descrip
    FROM BajaMotivo
    ORDER BY BajaMotivo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Banco_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Banco_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Banco.CodBanco, Banco.Descripcion
    FROM Banco
    ORDER BY Banco.Descripcion;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Bps2 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Bps2]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps2.CI)>0 THEN LEFT(Bps2.CI,CHARINDEX('-', Bps2.CI)-1) + SUBSTRING(Bps2.CI,CHARINDEX('-', Bps2.CI)+1,1) ELSE Bps2.CI END)) AS Cedula, Bps2.Apellido1, Bps2.Apellido2, Bps2.Nombres, Bps2.FechaNacimiento, Bps2.Sexo, Bps2.Nacionalidad, Bps2.Reservado1, Bps2.Reservado2, Bps2.Reservado3, Bps2.Reservado4
    FROM Bps2;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Bps3 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Bps3]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps3.CI)>0 THEN LEFT(Bps3.CI,CHARINDEX('-', Bps3.CI)-1) + SUBSTRING(Bps3.CI,CHARINDEX('-', Bps3.CI)+1,1) ELSE Bps3.CI END)) AS Cedula, Bps3.AcumulacionLaboral, Bps3.FechaIngreso, Bps3.SeguroSalud, Bps3.RemuneracionTipo, Bps3.HorasSemanales, Bps3.VinculoFuncional, Bps3.CodigoExoneracion, Bps3.ComputosEspeciales, Bps3.CausalBaja, Bps3.FechaBaja, Bps3.LocalEmpresa, Bps3.DiasTrabajados, Bps3.HorasTrabajadas, Bps3.Reservado1, Bps3.Reservado2, Bps3.Reservado3, Bps3.Reservado4, Bps3.Reservado5
    FROM Bps3;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Bps4 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Bps4]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CHARINDEX('-', CI) AS iCol, TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', Bps4.CI)>0 THEN LEFT(Bps4.CI,CHARINDEX('-', Bps4.CI)-1) + SUBSTRING(Bps4.CI,CHARINDEX('-', Bps4.CI)+1,1) ELSE Bps4.CI END)) AS Cedula, (CASE WHEN Bps4.Concepto='2' THEN '2' ELSE '1' END) AS Concepto, Bps4.Imponible
    FROM Bps4
    WHERE (((Bps4.Concepto)='1' Or (Bps4.Concepto)='2'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_CaseCasm =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_CaseCasm]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CONVERT(nvarchar(max),Casecasm.Campo1) AS CI, Casecasm.Campo2, Casecasm.Campo3, Casecasm.Campo4, Casecasm.Campo5
    FROM Casecasm
    WHERE (((Casecasm.Campo5) Like 'D.*'));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Certificacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Certificacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.*
    FROM Certificacion;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Certificacion_Nombre =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Certificacion_Nombre]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.*, Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + '' <> '' THEN ' ' + Afiliado.Apellido2 ELSE '' END) + ', ' + Afiliado.Nombres AS Nombre
    FROM Certificacion INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Certificador =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Certificador]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificador.*
    FROM Certificador;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Certificador_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Certificador_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificador.CodCertificador, Certificador.Descrip
    FROM Certificador
    ORDER BY Certificador.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Cristalin =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Cristalin]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,CONVERT(nvarchar(max),[Cristalin].[DOCUMENTO])) AS CI, Cristalin.[1ER APELLIDO], Cristalin.[2DO APELLIDO], Cristalin.[1ER NOMBRE], Cristalin.[2DO NOMBRE]
    FROM Cristalin;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Departamento_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Departamento_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Departamento.CodDepartamento, Departamento.Descrip
    FROM Departamento
    ORDER BY Departamento.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Empleo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Empleo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CI, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, Trabaja.FechaIngreso, Trabaja.FechaIngCasemed, Trabaja.FechaBaja, Trabaja.CodBajaMotivo, Trabaja.NroFichaEmpresa, Trabaja.IdTrabaja, Trabaja.Usr, Trabaja.Ts
    FROM Empresa INNER JOIN Trabaja ON Empresa.CodEmpresa = Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Empresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Empresa]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.*
    FROM Empresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_EmpresaPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_EmpresaPago]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmpresaPago.*
    FROM EmpresaPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Empresa_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Empresa_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.CodEmpresa, Empresa.Nombre
    FROM Empresa
    ORDER BY Empresa.Nombre;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Empresa_Desc_Real =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Empresa_Desc_Real]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.CodEmpresa, Empresa.Nombre
    FROM Empresa
    WHERE (((Empresa.Ficticia)= 0))
    ORDER BY Empresa.Nombre;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Especialidad_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Especialidad_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Especialidad.CodEspecialidad, Especialidad.Descrip
    FROM Especialidad
    ORDER BY Especialidad.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Export_BROU =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Export_BROU]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX),
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, SubsidioCabezal.ImpLiquido, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, Afiliado.CodBanco, Afiliado.NroCuenta, @pFecha AS Fecha
    FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
    WHERE (((Afiliado.CodBanco)=5) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar) AND ((SubsidioCabezal.ImpLiquido)>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Export_NBC =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Export_NBC]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX),
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, SubsidioCabezal.ImpLiquido, SubsidioCabezal.ImpNominal, SubsidioCabezal.ImpAguinaldo, Afiliado.CodBanco, Afiliado.NroCuenta, @pFecha AS Fecha
    FROM SubsidioCabezal INNER JOIN Afiliado ON SubsidioCabezal.CI = Afiliado.CI
    WHERE (((Afiliado.CodBanco)=1) AND ((SubsidioCabezal.Mes)=@pMes) AND ((SubsidioCabezal.Anio)=@pAnio) AND ((SubsidioCabezal.Liquidar)=@pLiquidar));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_FormaPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_FormaPago]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FormaPago.*
    FROM FormaPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_FormaPago_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_FormaPago_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FormaPago.CodFormaPago, FormaPago.Descrip
    FROM FormaPago
    ORDER BY FormaPago.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_GrupoEtario_Descrip =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_GrupoEtario_Descrip]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT GrupoEtario.EdadIni, GrupoEtario.Descrip
    FROM GrupoEtario
    ORDER BY GrupoEtario.EdadIni;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_ImpMax =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_ImpMax]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Max(Imponible.Importe) AS Importe
    FROM Imponible
    GROUP BY Imponible.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Imponible =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Imponible]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.CodEmpresa, Empresa.Nombre AS DescEmpresa, Imponible.Fechaingreso, ([Imponible].[Anio] + '/' + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2)) AS MesDbg, Imponible.Concepto, Imponible.DiasTrabajados, Imponible.Importe, Imponible.IdTrabaja, Imponible.Mes, Imponible.Anio, Imponible.Usr, Imponible.Ts
    FROM Imponible INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa
    ORDER BY ([Imponible].[Anio] + '/' + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2)) DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Imponible_Ult =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Imponible_Ult]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, I.CodEmpresa, I.Mes, I.Anio, I.Importe
    FROM Imponible AS I
    WHERE I.Concepto='1'
    AND I.Mes = MONTH(DATEADD(month, -2, CAST(GETDATE() AS date)))
    AND I.Anio = YEAR(DATEADD(month, -2, CAST(GETDATE() AS date)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Imponible_Ult_Ant =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Imponible_Ult_Ant]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, I.CodEmpresa, I.Mes, I.Anio, I.Importe
    FROM Imponible AS I
    WHERE (((I.Concepto)='1') AND (EXISTS(SELECT 1 FROM Imponible I1
    WHERE I1.CI = I.CI AND I1.CodEmpresa = I.CodEmpresa AND I1.Concepto = '1'
    GROUP BY I1.CI, I1.CodEmpresa
    HAVING MAX(I1.ANIOMES) = I.AnioMes)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_InformeEstadistico =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_InformeEstadistico]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT InformeEstadistico.*
    FROM InformeEstadistico
    ORDER BY InformeEstadistico.Orden;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_MaxImp_Afiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_MaxImp_Afiliado]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, MIN(Imponible.CodEmpresa) AS CodEmpresa, MIN(Imponible.Mes) AS Mes, MIN(Imponible.Anio) AS Anio, Max(Imponible.Importe) AS Importe
    FROM Imponible
    GROUP BY Imponible.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Mutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Mutualista]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Mutualista.*
    FROM Mutualista
    WHERE (((Mutualista.CodMutualista)>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Mutualista_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Mutualista_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Mutualista.CodMutualista, Mutualista.Descrip
    FROM Mutualista
    ORDER BY Mutualista.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Patologia =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Patologia]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Patologia.*
    FROM Patologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Patologia_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Patologia_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Patologia.CodPatologia, Patologia.Descrip
    FROM Patologia
    ORDER BY Patologia.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_Prestacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_Prestacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Prestacion.*, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
    FROM Prestacion INNER JOIN Afiliado ON Prestacion.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestacionTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_PrestacionTipo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PrestacionTipo.*
    FROM PrestacionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestacionTipo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_PrestacionTipo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PrestacionTipo.CodPrestacionTipo, PrestacionTipo.Descrip
    FROM PrestacionTipo
    ORDER BY PrestacionTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_RegimenAporte_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_RegimenAporte_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RegimenAporte.CodRegimenAporte, RegimenAporte.Descrip
    FROM RegimenAporte
    ORDER BY RegimenAporte.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_RegimenJubilatorio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_RegimenJubilatorio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RegimenJubilatorio.*
    FROM RegimenJubilatorio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_RegimenJubilatorio_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_RegimenJubilatorio_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RegimenJubilatorio.CodRegimenJubilatorio, RegimenJubilatorio.Descrip
    FROM RegimenJubilatorio
    ORDER BY RegimenJubilatorio.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_ReintegroMutual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_ReintegroMutual]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ReintegroMutual.*, [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) + ', ' + [Afiliado].[Nombres] AS DescAfiliado
    FROM ReintegroMutual INNER JOIN Afiliado ON ReintegroMutual.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SalidaTipo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SalidaTipo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SalidaTipo.*
    FROM SalidaTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SalidaTipo_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SalidaTipo_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SalidaTipo.CodSalidaTipo, SalidaTipo.Descrip
    FROM SalidaTipo
    ORDER BY SalidaTipo.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SituacionMutual =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SituacionMutual]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SituacionMutual.*
    FROM SituacionMutual;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SituacionMutual_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SituacionMutual_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SituacionMutual.CodSituacionMutual, SituacionMutual.Descrip
    FROM SituacionMutual
    ORDER BY SituacionMutual.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SituacionPago =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SituacionPago]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SituacionPago.*
    FROM SituacionPago;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SituacionPago_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SituacionPago_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SituacionPago.CodSituacionPago, SituacionPago.Descrip
    FROM SituacionPago
    ORDER BY SituacionPago.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SubsidioItem =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SubsidioItem]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItem.*, SubsidioItemCod.Tipo
    FROM SubsidioItem INNER JOIN SubsidioItemCod ON SubsidioItem.CodSubsidioItemCod = SubsidioItemCod.CodSubsidioItemCod;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SubsidioItemCodXCI =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SubsidioItemCodXCI]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod_Afiliado.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod_Afiliado.Vigencia AS FechaBaja, SubsidioItemCod.ModificaNominal, SubsidioItemCod_Afiliado.CI, SubsidioItemCod.ModificaNominal
    FROM SubsidioItemCod INNER JOIN SubsidioItemCod_Afiliado ON SubsidioItemCod.CodSubsidioItemCod = SubsidioItemCod_Afiliado.CodSubsidioItemCod;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SubsidioItemCod_Afiliado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SubsidioItemCod_Afiliado]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod_Afiliado.*, Afiliado.Apellido1 + ', ' + Afiliado.Nombres AS Nombre
    FROM SubsidioItemCod_Afiliado INNER JOIN Afiliado ON SubsidioItemCod_Afiliado.CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SubsidioItemCod_Desc =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SubsidioItemCod_Desc]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip
    FROM SubsidioItemCod
    ORDER BY SubsidioItemCod.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_SubsidioXMes =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_SubsidioXMes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezal.CI, SubsidioCabezal.Anio, SubsidioCabezal.Mes, Sum(SubsidioCabezal.ImpNominal) AS ImpNominal, Sum(SubsidioCabezal.ImpAguinaldo) AS ImpAguinaldo
    FROM SubsidioCabezal
    GROUP BY SubsidioCabezal.CI, SubsidioCabezal.Anio, SubsidioCabezal.Mes;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_TrabajaActivo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_TrabajaActivo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.*
    FROM Trabaja
    WHERE (((Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_TrabajaUltimo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rs_TrabajaUltimo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CI, Trabaja.CodEmpresa, MAX(Trabaja.FechaIngreso) AS FechaIngreso, MAX(Trabaja.FechaBaja) AS FechaBaja, MAX(Trabaja.CodBajaMotivo) AS CodBajaMotivo, MAX(Trabaja.NroFichaEmpresa) AS NroFichaEmpresa, MAX(Trabaja.IdTrabaja) AS IdTrabaja, MAX(Trabaja.FechaIngCasemed) AS FechaIngCasemed, MAX(Trabaja.Usr) AS Usr, MAX(Trabaja.Ts) AS Ts
    FROM Trabaja
    GROUP BY Trabaja.CI, Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Update_Bps_Mutualista =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Update_Bps_Mutualista]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Afiliado SET Afiliado.CodMutualista = [Mutualista].[CodMutualista] FROM Mutualista INNER JOIN (BpsFormat INNER JOIN Afiliado ON BpsFormat.Cedula = Afiliado.CI) ON Mutualista.Descrip = BpsFormat.Mutualista
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: wx_Update_ValorJornal =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_wx_Update_ValorJornal]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE SubsidioCabezal SET SubsidioCabezal.ValorJornal = [s].[SumaDeValorJornal] FROM SubsidioCabezal INNER JOIN xw_Suma_ValorJornal AS s ON SubsidioCabezal.IdSubsidio = s.IdSubsidio
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xEmpresaCantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xEmpresaCantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CodEmpresa, Count(*) AS Cantidad
    FROM Trabaja
    GROUP BY Trabaja.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xEmpresaPromedio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xEmpresaPromedio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CodEmpresa, FORMAT(Avg(Imponible.Importe), '0.00') AS PromedioDeImporte
    FROM Imponible
    WHERE TRY_CONVERT(float,Anio + RIGHT('00' + CONVERT(varchar(2), Mes), 2)) Between 199906 And 199911
    GROUP BY Imponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xEmpresaPromedioTodo =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xEmpresaPromedioTodo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CodEmpresa, Avg(Imponible.Importe) AS PromedioDeImporte
    FROM Imponible
    GROUP BY Imponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xMutualistaCantidad =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xMutualistaCantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CodMutualista, Count(*) AS Cantidad
    FROM Afiliado
    GROUP BY Afiliado.CodMutualista;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xSinImponiblexEmpresa =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xSinImponiblexEmpresa]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Imponible.CodEmpresa
    FROM Imponible
    GROUP BY Imponible.CI, Imponible.CodEmpresa
    HAVING (((Sum(Imponible.Importe))=0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xw_Suma_ValorJornal1 =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xw_Suma_ValorJornal1]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioCabezalEmpresa.IdSubsidio, Sum(SubsidioCabezalEmpresa.ValorJornal) AS SumaDeValorJornal
    FROM SubsidioCabezalEmpresa
    GROUP BY SubsidioCabezalEmpresa.IdSubsidio;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: zGrupoSubsidio =====
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_zGrupoSubsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Anio, (CASE WHEN ValorJornal*30<=5000 THEN '0:5' WHEN ValorJornal*30>5000 And ValorJornal*30<=10000 THEN '5:10' WHEN ValorJornal*30>10000 And ValorJornal*30<=20000 THEN '10:20' WHEN ValorJornal*30>20000 And ValorJornal*30<=30000 THEN '20:30' WHEN ValorJornal*30>30000 And ValorJornal*30<=40000 THEN '30:40' WHEN ValorJornal*30>40000 And ValorJornal*30<=50000 THEN '40:50' WHEN ValorJornal*30>50000 And ValorJornal*30<=60000 THEN '50:60' WHEN ValorJornal*30>60000 THEN '60:' ELSE NULL END) AS Sueldo, Dias
    FROM SubsidioCabezal
    WHERE Liquidar= 0;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_Imponible_6_Meses =====
-- DependsOn: 200_Imponible
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_Imponible_6_Meses]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT q_200_Imponible.CI, TRY_CONVERT(int,Sum(q_200_Imponible.Importe)/6) AS Importe
    FROM [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2))) Between @pMesIni And @pMes) AND ((q_200_Imponible.Concepto)='1'))
    GROUP BY q_200_Imponible.CI
    HAVING (((TRY_CONVERT(int,Sum(q_200_Imponible.[Importe])/6))>=(1.25*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_Imponible_Ult_Mes =====
-- DependsOn: 200_Imponible
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_Imponible_Ult_Mes]
    @pMes INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT q_200_Imponible.CI, TRY_CONVERT(int,Sum(q_200_Imponible.Importe)) AS Importe
    FROM [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2)))=@pMes) AND ((q_200_Imponible.Concepto)='1'))
    GROUP BY q_200_Imponible.CI
    HAVING (((TRY_CONVERT(int,Sum(q_200_Imponible.[Importe])))>=(1.25*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 210_Insert_600_Rpt_BPS =====
-- DependsOn: 210_TotTributo, 210_TotImpEmp, 210_MontoGrabado, 210_ImpRetTotal, 210_ImpRetPatronal, 210_ImpRetObrero, 210_SubsidioCantidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_210_Insert_600_Rpt_BPS]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_BPS] ( Mes, Anio, ImpRetObrero, ImpRetPatronal, TotImpRet, Monto, TotImpMut, TributoMonto, Cantidad, TributoTotImpMut )
    SELECT @pMes AS Expr1, @pAnio AS Expr2, [210_ImpRetObrero].Importe, [210_ImpRetPatronal].Importe, [210_ImpRetTotal].Importe, [210_MontoGrabado].Importe, [210_TotImpEmp].Importe, [210_TotTributo].Importe, S.Cantidad, [210_TotImpEmp].TributoTotImpMut
    FROM [210_TotTributo](@pMes, @pAnio, @pLiquidar) RIGHT JOIN ([210_TotImpEmp](@pMes, @pAnio) RIGHT JOIN ([210_MontoGrabado](@pMes, @pAnio, @pLiquidar) RIGHT JOIN ([210_ImpRetTotal](@pMes, @pAnio, @pLiquidar) RIGHT JOIN ([210_ImpRetPatronal](@pMes, @pAnio, @pLiquidar) RIGHT JOIN ([210_ImpRetObrero](@pMes, @pAnio, @pLiquidar) RIGHT JOIN [210_SubsidioCantidad](@pMes, @pAnio, @pLiquidar) AS S ON ([210_ImpRetObrero].Mes = S.Mes) AND ([210_ImpRetObrero].Anio = S.Anio)) ON ([210_ImpRetPatronal].Mes = S.Mes) AND ([210_ImpRetPatronal].Anio = S.Anio)) ON ([210_ImpRetTotal].Mes = S.Mes) AND ([210_ImpRetTotal].Anio = S.Anio)) ON ([210_MontoGrabado].Mes = S.Mes) AND ([210_MontoGrabado].Anio = S.Anio)) ON ([210_TotImpEmp].Mes = S.Mes) AND ([210_TotImpEmp].Anio = S.Anio)) ON ([210_TotTributo].Mes = S.Mes) AND ([210_TotTributo].Anio = S.Anio);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 220_AfiliadoPromedio =====
-- DependsOn: 220_AfiliadoImponibleMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_220_AfiliadoPromedio]
    @pCI INT,
    @pMes INT,
    @pMesIni INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Avg([220_AfiliadoImponibleMes].Importe) AS Promedio
    FROM [acc_sgpa_220_AfiliadoImponibleMes_q](@pCI, @pMes, @pMesIni) AS [220_AfiliadoImponibleMes];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 250_Control_Aporte =====
-- DependsOn: 250_ActivosXEmpresaAUnaFecha, 250_AportantesAUnMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_250_Control_Aporte]
    @pFecha DATETIME2(0),
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [250_ActivosXEmpresaAUnaFecha].Nombre, [250_ActivosXEmpresaAUnaFecha].Cantidad AS Activos, [250_AportantesAUnMes].Cantidad AS Aportantes
    FROM [acc_sgpa_250_ActivosXEmpresaAUnaFecha_q](@pFecha) AS [250_ActivosXEmpresaAUnaFecha] LEFT JOIN [acc_sgpa_250_AportantesAUnMes_q](@pAnioMes) AS [250_AportantesAUnMes] ON [250_ActivosXEmpresaAUnaFecha].CodEmpresa = [250_AportantesAUnMes].CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Subsidio =====
-- DependsOn: 300_Rpt_Subsidio
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Subsidio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sgpa_300_Rpt_Subsidio_q]
    WHERE [Mes] = 7 And [Anio] = 2007 And [CI] = 41856014 And Liquidar = 1;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_AfiliadoDiasImporte =====
-- DependsOn: 300_TrabajaActivo, 300_AfiliadoAporteOk
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_AfiliadoDiasImporte]
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CI, Sum(Imponible.DiasTrabajados) AS Dias, Imponible.CodEmpresa, Sum(Imponible.Importe) AS Importe, Sum(Imponible.Importe) / (((@pMesFin / 100 - @pMesIni / 100) * 12 + (@pMesFin % 100 - @pMesIni % 100) + 1) * 30.0) AS Promedio
    FROM ((Imponible INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo] ON (Imponible.CI = [300_TrabajaActivo].CI) AND (Imponible.CodEmpresa = [300_TrabajaActivo].CodEmpresa) AND (Imponible.Fechaingreso = [300_TrabajaActivo].FechaIngreso)) INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk] ON ([300_TrabajaActivo].FechaIngreso = [300_AfiliadoAporteOk].Fechaingreso) AND ([300_TrabajaActivo].CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa) AND ([300_TrabajaActivo].CI = [300_AfiliadoAporteOk].CI) AND (Empresa.CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa)
    WHERE (((Imponible.Concepto)='1') AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMesFin) AND ((Imponible.CI)=@pCI) AND ((Empresa.Liquidar)=@pLiquidar) AND (([300_AfiliadoAporteOk].Cantidad)>=@pDias))
    GROUP BY Imponible.CI, Imponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_InsertSubsidioImponible =====
-- DependsOn: 300_TrabajaActivo, 300_AfiliadoAporteOk
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_InsertSubsidioImponible]
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pUsr NVARCHAR(MAX),
    @pIdSubsidio INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pCodCasemed INT,
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SubsidioImponible ( IdSubsidio, Dias, CodEmpresa, Importe, Mes, Anio, Usr, Ts )
    SELECT @pIdSubsidio AS Expr3, Imponible.DiasTrabajados AS Dias, Imponible.CodEmpresa, Imponible.Importe AS Importe, Imponible.Mes, Imponible.Anio, @pUsr AS Expr1, SYSDATETIME() AS Expr2
    FROM ((Imponible INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo] ON (Imponible.CI = [300_TrabajaActivo].CI) AND (Imponible.CodEmpresa = [300_TrabajaActivo].CodEmpresa) AND (Imponible.Fechaingreso = [300_TrabajaActivo].FechaIngreso)) INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk] ON ([300_TrabajaActivo].FechaIngreso = [300_AfiliadoAporteOk].Fechaingreso) AND ([300_TrabajaActivo].CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa) AND ([300_TrabajaActivo].CI = [300_AfiliadoAporteOk].CI)
    WHERE (((Imponible.CodEmpresa)<>@pCodCasemed) AND ((Imponible.CI)=@pCI) AND ((Imponible.Concepto)='1') AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMesFin) AND ((Empresa.Liquidar)=@pLiquidar) AND (([300_AfiliadoAporteOk].Cantidad)>=@pDias));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_InsertSubsidioImponibleCasemed =====
-- DependsOn: 300_TrabajaActivo, 300_AfiliadoAporteOk
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_InsertSubsidioImponibleCasemed]
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pUsr NVARCHAR(MAX),
    @pIdSubsidio INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pCodCasemed INT,
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO SubsidioImponible ( IdSubsidio, Dias, CodEmpresa, Importe, Mes, Anio, Usr, Ts )
    SELECT @pIdSubsidio AS Expr3, Imponible.DiasTrabajados AS Dias, Imponible.CodEmpresa, Imponible.Importe AS Importe, Imponible.Mes, Imponible.Anio, @pUsr AS Expr1, SYSDATETIME() AS Expr2
    FROM ((Imponible INNER JOIN [acc_sgpa_300_TrabajaActivo_q](@pMes) AS [300_TrabajaActivo] ON (Imponible.CI = [300_TrabajaActivo].CI) AND (Imponible.CodEmpresa = [300_TrabajaActivo].CodEmpresa) AND (Imponible.Fechaingreso = [300_TrabajaActivo].FechaIngreso)) INNER JOIN Empresa ON Imponible.CodEmpresa = Empresa.CodEmpresa) INNER JOIN [acc_sgpa_300_AfiliadoAporteOk_q](@pMesIniImp, @pMesFin) AS [300_AfiliadoAporteOk] ON ([300_TrabajaActivo].FechaIngreso = [300_AfiliadoAporteOk].Fechaingreso) AND ([300_TrabajaActivo].CodEmpresa = [300_AfiliadoAporteOk].CodEmpresa) AND ([300_TrabajaActivo].CI = [300_AfiliadoAporteOk].CI)
    WHERE (((Imponible.CodEmpresa)=@pCodCasemed) AND ((Imponible.CI)=@pCI) AND ((Imponible.Concepto)='1') AND ((TRY_CONVERT(float,CONVERT(nvarchar(max),[Imponible].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [Imponible].[Mes]), 2))) Between @pMesIni And @pMesFin) AND ((Empresa.Liquidar)=@pLiquidar) AND (([300_AfiliadoAporteOk].Cantidad)>=@pDias));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Promedio_Mes =====
-- DependsOn: 400_Suma_Importe
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Promedio_Mes]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio, Avg(acc_sgpa_400_Suma_Importe_q.Importe) AS Importe
    FROM [acc_sgpa_400_Suma_Importe_q]
    GROUP BY acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio
    ORDER BY TRY_CONVERT(float,Anio + Mes);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Promedio_Mes_Puesto =====
-- DependsOn: 400_Suma_Puestos
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Promedio_Mes_Puesto]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio, Avg(acc_sgpa_400_Suma_Puestos_q.Importe) AS Importe
    FROM [acc_sgpa_400_Suma_Puestos_q]
    GROUP BY acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio
    ORDER BY TRY_CONVERT(float,Anio + RIGHT('00' + CONVERT(varchar(2), Mes), 2));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 460_AfiliadoPromedioxCI =====
-- DependsOn: 460_IMS_Actual, 460_Imponible
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_460_AfiliadoPromedioxCI]
    @pCI INT,
    @pAnioMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_460_Imponible_q.CI, Avg((acc_sgpa_460_Imponible_q.[Importe]*[460_IMS_Actual].[Importe])/[IMS].[Importe]) AS Promedio
    FROM [acc_sgpa_460_IMS_Actual_q](@pAnioMes) AS [460_IMS_Actual], IMS INNER JOIN [acc_sgpa_460_Imponible_q] ON (IMS.Anio = acc_sgpa_460_Imponible_q.Anio) AND (IMS.Mes = acc_sgpa_460_Imponible_q.Mes)
    GROUP BY acc_sgpa_460_Imponible_q.CI
    HAVING (((acc_sgpa_460_Imponible_q.CI)=@pCI));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Rpt_Ficha_Certificacion =====
-- DependsOn: 480_F_Ult_Certif
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Rpt_Ficha_Certificacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [600_Afiliado_Certificado].CI, [600_Afiliado_Certificado].Nombres, [600_Afiliado_Certificado].Apellido1, [600_Afiliado_Certificado].Apellido2, [600_Afiliado_Certificado].FechaNacimiento, [600_Afiliado_Certificado].Sexo, [600_Afiliado_Certificado].CodMutualista, [600_Afiliado_Certificado].DescMutualista, [600_Afiliado_Certificado].Especialidad, [600_Afiliado_Certificado].Promedio, [600_Afiliado_Certificado].Empleos, [600_Afiliado_Certificado].DiaProrroga, DATEADD(day,[600_Afiliado_Certificado].DiasUltPro,[600_Afiliado_Certificado].F_Ult_Prorroga) AS F_Ult_Prorroga, [600_Afiliado_Certificado_Afeccion].CodAfeccionTipo, [600_Afiliado_Certificado_Afeccion].DescAfeccionTipo, [600_Afiliado_Certificado_Afeccion].Cantidad, [600_Afiliado_Certificado_Afeccion].Dias, acc_sgpa_480_F_Ult_Certif_q.F_Ult_Certificacion
    FROM ([600_Afiliado_Certificado] INNER JOIN [acc_sgpa_480_F_Ult_Certif_q] ON [600_Afiliado_Certificado].CI = acc_sgpa_480_F_Ult_Certif_q.CI) INNER JOIN [600_Afiliado_Certificado_Afeccion] ON [600_Afiliado_Certificado].CI = [600_Afiliado_Certificado_Afeccion].CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Prorrogas =====
-- DependsOn: 480_SumaProrrogas
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Prorrogas]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CP.CI, CP.Fecha AS F_Ult_Prorroga, acc_sgpa_480_SumaProrrogas_q.Dias, CP.Dias AS DiasUltPro
    FROM CertificacionProrroga AS CP INNER JOIN [acc_sgpa_480_SumaProrrogas_q] ON CP.CI = acc_sgpa_480_SumaProrrogas_q.CI
    WHERE ((EXISTS(SELECT 1 FROM CertificacionProrroga AS CP2
    WHERE CP.CI = CP2.CI
    GROUP BY CP2.CI HAVING MAX(CP2.Fecha) = CP.Fecha)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 490_SubsidioImporte =====
-- DependsOn: 490_Subsidio
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_490_SubsidioImporte]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI AS CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' ELSE '' END) + [Afiliado].[Apellido2] AS DescAfiliado, acc_sgpa_490_Subsidio_q.ImpLiquido AS Importe
    FROM Afiliado INNER JOIN [acc_sgpa_490_Subsidio_q] ON Afiliado.CI = acc_sgpa_490_Subsidio_q.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Cargado_HL =====
-- DependsOn: 500_Rpt_Cargado_HL_Det
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Cargado_HL]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sgpa_500_Rpt_Cargado_HL_Det_q]
    WHERE Mes = 07 AND Anio = 2013 AND CodEmpresa = 900;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_DiasCertificacion =====
-- DependsOn: 500_Prorrogas, 500_Rpt_Certificacion_UltFecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_DiasCertificacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.CI, AfeccionTipo.CodAfeccionTipo, MIN(AfeccionTipo.Descrip) AS DescAfeccionTipo, MIN(Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END)) AS DescAfiliado, Sum(DATEDIFF(day,Certificacion.[FechaIni],Certificacion.[FechaFin])+1) AS Dias, Count(*) AS Cantidad, MIN(acc_sgpa_500_Prorrogas_q.Dias) AS Prorrogas, MAX(acc_sgpa_500_Rpt_Certificacion_UltFecha_q.FechaFin) AS F_Ult_Certif, MAX(acc_sgpa_500_Prorrogas_q.Fecha) AS F_Ult_Pro
    FROM (((((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo) LEFT JOIN [acc_sgpa_500_Prorrogas_q] ON Certificacion.CI = acc_sgpa_500_Prorrogas_q.CI) INNER JOIN [acc_sgpa_500_Rpt_Certificacion_UltFecha_q] ON Afiliado.CI = acc_sgpa_500_Rpt_Certificacion_UltFecha_q.CI
    WHERE ( Certificacion.[CI] = 11391501 )
    GROUP BY Certificacion.CI, AfeccionTipo.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_DiasCertificacion_S =====
-- DependsOn: 500_Prorrogas, 500_Rpt_Certificacion_UltFecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_S]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Certificacion.CI, AfeccionTipo.CodAfeccionTipo, MIN(AfeccionTipo.Descrip) AS DescAfeccionTipo, MIN(Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END)) AS DescAfiliado, Sum(DATEDIFF(day,Certificacion.[FechaIni],Certificacion.[FechaFin])+1) AS Dias, Count(*) AS Cantidad, MIN(acc_sgpa_500_Prorrogas_q.Dias) AS Prorrogas, MAX(acc_sgpa_500_Rpt_Certificacion_UltFecha_q.FechaFin) AS F_Ult_Certif, MAX(acc_sgpa_500_Prorrogas_q.Fecha) AS F_Ult_Pro
    FROM (((((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN Certificador ON Certificacion.CodCertificador = Certificador.CodCertificador) INNER JOIN Afiliado ON Certificacion.CI = Afiliado.CI) INNER JOIN SalidaTipo ON Certificacion.CodSalidaTipo = SalidaTipo.CodSalidaTipo) LEFT JOIN [acc_sgpa_500_Prorrogas_q] ON Certificacion.CI = acc_sgpa_500_Prorrogas_q.CI) INNER JOIN [acc_sgpa_500_Rpt_Certificacion_UltFecha_q] ON Afiliado.CI = acc_sgpa_500_Rpt_Certificacion_UltFecha_q.CI
    GROUP BY Certificacion.CI, AfeccionTipo.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_DetalleSubsidio_Tmp =====
-- DependsOn: 500_Rpt_DetalleSubsidio
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_DetalleSubsidio_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN LEN(CONVERT(nvarchar(max),[D].[CI]))>7 THEN FORMAT([D].[CI],'@\.@@@\.@@@-@') ELSE FORMAT([D].[CI],'@@@\.@@@-@') END) AS CIFormat, D.CI, D.DescAfiliado, D.Mes, D.Anio, D.CodEmpresa, D.DescEmpresa, D.Dias, D.Importe, D.MesCabezal, D.AnioCabezal, D.DiasSubsidio, D.IdSubsidio
    FROM [acc_sgpa_500_Rpt_DetalleSubsidio_q] AS D
    WHERE [MesCabezal] = 08 And [AnioCabezal] = 2013 And Liquidar = 1;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 506_Export_SubsidioConBPS =====
-- DependsOn: 506_Rpt_Subsidio, 506_Rpt_LiquidacionBPS
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_506_Export_SubsidioConBPS]
    @pMes INT,
    @pAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_506_Rpt_Subsidio_q.CI, acc_sgpa_506_Rpt_Subsidio_q.Dias, acc_sgpa_506_Rpt_Subsidio_q.Nombres, acc_sgpa_506_Rpt_Subsidio_q.Apellido1, acc_sgpa_506_Rpt_Subsidio_q.Apellido2, acc_sgpa_506_Rpt_Subsidio_q.FechaNacimiento, acc_sgpa_506_Rpt_Subsidio_q.IdSubsidio, acc_sgpa_506_Rpt_Subsidio_q.NroRecibo, acc_sgpa_506_Rpt_Subsidio_q.FechaIni, acc_sgpa_506_Rpt_Subsidio_q.FechaFin, acc_sgpa_506_Rpt_Subsidio_q.FechaIniSubsidio, acc_sgpa_506_Rpt_Subsidio_q.FechaFinSubsidio, acc_sgpa_506_Rpt_Subsidio_q.ImpNominal, acc_sgpa_506_Rpt_Subsidio_q.ImpAguinaldo, acc_sgpa_506_Rpt_Subsidio_q.ImpLiquido, acc_sgpa_506_Rpt_Subsidio_q.Jornal70, acc_sgpa_506_Rpt_Subsidio_q.Aguinaldo70, acc_sgpa_506_Rpt_Subsidio_q.DiasBPS, acc_sgpa_506_Rpt_Subsidio_q.LiquidoBPS, acc_sgpa_506_Rpt_Subsidio_q.LiquidoPagar, acc_sgpa_506_Rpt_Subsidio_q.Banco, acc_sgpa_506_Rpt_Subsidio_q.NroCuenta, [506_Rpt_LiquidacionBPS].MONTO_TOTAL, [506_Rpt_LiquidacionBPS].LIQUIDO, [506_Rpt_LiquidacionBPS].MES_DE_CARGO, [506_Rpt_LiquidacionBPS].NOM_EMPRESA, [506_Rpt_LiquidacionBPS].PCT_POR_EMPRESA, [506_Rpt_LiquidacionBPS].FECHA_PER_DESDE, [506_Rpt_LiquidacionBPS].FECHA_PER_HASTA, [506_Rpt_LiquidacionBPS].[N_ ENTREGA], [506_Rpt_LiquidacionBPS].FECHA_DE_ENTREGA, acc_sgpa_506_Rpt_Subsidio_q.EMail
    FROM [acc_sgpa_506_Rpt_Subsidio_q] LEFT JOIN [acc_sgpa_506_Rpt_LiquidacionBPS_q](@pMes, @pAnio) AS [506_Rpt_LiquidacionBPS] ON acc_sgpa_506_Rpt_Subsidio_q.CI = [506_Rpt_LiquidacionBPS].CI
    WHERE (((acc_sgpa_506_Rpt_Subsidio_q.Mes)=@pMes) AND ((acc_sgpa_506_Rpt_Subsidio_q.Anio)=@pAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 756_NoBaja =====
-- DependsOn: 765_CertificacionContinua, 765_CertificacionEmpalma
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_756_NoBaja]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CI FROM [acc_sgpa_765_CertificacionContinua_q](@pFecha)
    UNION SELECT CI FROM [acc_sgpa_765_CertificacionEmpalma_q](@pFecha);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 815_AfiliadoImponible =====
-- DependsOn: 800_AfiliadoImponible_Mes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_815_AfiliadoImponible]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Importe
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_CI_Todos =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_CI_Todos]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI AS Cantidad
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes
    GROUP BY I.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_Promedio_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_Promedio_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg(I.Importe) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes
    GROUP BY I.CI
    HAVING (Avg([I].[Importe])=0);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_Promedio_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_Promedio_UltMes]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, TRY_CONVERT(float,Sum(I.Importe)) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))=0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>0_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__0_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>0_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__0_UltMes]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, TRY_CONVERT(float,Sum([I].[Importe])) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>=(1.25*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>=(1.25*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 803_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_803_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>=(20*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 803_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_803_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((TRY_CONVERT(float,Sum([I].[Importe])))>=(20*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>0_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__0_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>0_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__0_UltMes]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))  =  @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>=(20*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) = @pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>=(20*@pSMN)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_Afiliado<125_Pct_Ult6 =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_Afiliado_125_Pct_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25 * @pSMN * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 10)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 20)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 30)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 40)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 50)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 60)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 70)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 80)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 90)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2))) Between @pMesIni And @pMes))
    GROUP BY I.CI
    HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*@pSMN));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_Afiliado<125_Pct_UltMes =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_Afiliado_125_Pct_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg([I].[Importe]) AS Importe, (CASE WHEN Avg([I].[Importe]) > 0 And Avg([I].[Importe]) <= ((1.25 * @pSMN * 10)/100) THEN 'Mayor que 0 hasta 10%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 10)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 20)/100) THEN 'Mayor que 10% hasta 20%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 20)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 30)/100) THEN 'Mayor que 20% hasta 30%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 30)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 40)/100) THEN 'Mayor que 30% hasta 40%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 40)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 50)/100) THEN 'Mayor que 40% hasta 50%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 50)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 60)/100) THEN 'Mayor que 50% hasta 60%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 60)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 70)/100) THEN 'Mayor que 60% hasta 70%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 70)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 80)/100) THEN 'Mayor que 70% hasta 80%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 80)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 90)/100) THEN 'Mayor que 80% hasta 90%' WHEN Avg([I].[Importe]) > ((1.25 * @pSMN * 90)/100) And Avg([I].[Importe]) <= ((1.25 * @pSMN * 100)/100) THEN 'Mayor que 90% hasta 100%' ELSE NULL END) AS Grupo
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING Avg([I].[Importe])>0 And Avg([I].[Importe])<((1.25*@pSMN));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 814_AfiliadoImponibleFranja =====
-- DependsOn: 800_AfiliadoImponible_Mes_Fecha
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_814_AfiliadoImponibleFranja]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN Avg([I].[Importe])<=10000 THEN 'Mas de 0 hasta 10.000' WHEN Avg([I].[Importe])>10000 And Avg([I].[Importe])<=20000 THEN 'Mas de 10.000 hasta 20.000' WHEN Avg([I].[Importe])>20000 THEN 'Mas de 20.000' ELSE NULL END) AS Grupo, I.CI, Avg([I].[Importe]) AS Importe
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_Fecha_q](@pCodEmpresa, @pMes) AS I
    WHERE (((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)))=@pMes))
    GROUP BY I.CI
    HAVING (((Avg([I].[Importe]))>0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 807_Insert_CertificadosEspecialidad =====
-- DependsOn: 807_CertificadosEspecialidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_807_Insert_CertificadosEspecialidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip, Codigo )
    SELECT [807_CertificadosEspecialidad].Cantidad, [807_CertificadosEspecialidad].Descripcion, [807_CertificadosEspecialidad].Codigo
    FROM [807_CertificadosEspecialidad](NULL, NULL)
    ORDER BY [807_CertificadosEspecialidad].Cantidad DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 808_Insert_CertificadosAfeccion =====
-- DependsOn: 808_CertificadosAfecciones
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_808_Insert_CertificadosAfeccion]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip, Codigo )
    SELECT [808_CertificadosAfecciones].Cantidad, [808_CertificadosAfecciones].Descripcion, [808_CertificadosAfecciones].Codigo
    FROM [808_CertificadosAfecciones](NULL, NULL, NULL)
    ORDER BY [808_CertificadosAfecciones].Cantidad DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 812_Insert_AfiliadoActivoEspecialidad =====
-- DependsOn: 812_AfiliadoActivoEspecialidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_812_Insert_AfiliadoActivoEspecialidad]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Cantidad, Descrip )
    SELECT acc_sgpa_812_AfiliadoActivoEspecialidad_q.Codigo, acc_sgpa_812_AfiliadoActivoEspecialidad_q.Cantidad, acc_sgpa_812_AfiliadoActivoEspecialidad_q.Descripcion
    FROM [acc_sgpa_812_AfiliadoActivoEspecialidad_q] AS acc_sgpa_812_AfiliadoActivoEspecialidad_q;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 812_Insert_AfiliadosEspecialidad =====
-- DependsOn: 812_AfiliadosEspecialidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_812_Insert_AfiliadosEspecialidad]
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [812_AfiliadosEspecialidad].Codigo, [812_AfiliadosEspecialidad].Descripcion, [812_AfiliadosEspecialidad].Cantidad
    FROM [812_AfiliadosEspecialidad](NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 813_CertificadosAfeccion =====
-- DependsOn: 813_CertificacionAfeccionDistintas
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_813_CertificadosAfeccion]
    @pCodPatologia NVARCHAR(MAX),
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [813_CertificacionAfeccionDistintas].CodAfeccionTipo AS Codigo, MIN(AfeccionTipo.Descrip) AS Descrip, Count(*) AS Cantidad
    FROM ((Afiliado INNER JOIN [acc_sgpa_813_CertificacionAfeccionDistintas_q](@pFechaIni, @pFechaFin) AS [813_CertificacionAfeccionDistintas] ON Afiliado.CI = [813_CertificacionAfeccionDistintas].CI) INNER JOIN AfeccionTipo ON [813_CertificacionAfeccionDistintas].CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo
    WHERE (((AfeccionGrupo.CodPatologia)=(CASE WHEN @pCodPatologia Is NOT Null THEN @pCodPatologia ELSE [AfeccionGrupo].[CodPatologia] END)))
    GROUP BY [813_CertificacionAfeccionDistintas].CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 816_Insert_Certificados_GrupoAfeccion =====
-- DependsOn: 816_Certificados_GrupoAfeccion
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_816_Insert_Certificados_GrupoAfeccion]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip, Codigo )
    SELECT [816_Certificados_GrupoAfeccion].Cantidad, [816_Certificados_GrupoAfeccion].Descripcion, [816_Certificados_GrupoAfeccion].Codigo
    FROM [816_Certificados_GrupoAfeccion](NULL, NULL, NULL)
    ORDER BY [816_Certificados_GrupoAfeccion].Cantidad DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 817_Insert_Certificados_Patologia =====
-- DependsOn: 817_Certificados_Patologia
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_817_Insert_Certificados_Patologia]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip, Codigo )
    SELECT [817_Certificados_Patologia].Cantidad, [817_Certificados_Patologia].Descripcion, [817_Certificados_Patologia].Codigo
    FROM [817_Certificados_Patologia](NULL, NULL)
    ORDER BY [817_Certificados_Patologia].Cantidad DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 818_Insert_Certificados_Patologia =====
-- DependsOn: 818_Certificados_Patologia
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_818_Insert_Certificados_Patologia]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [818_Certificados_Patologia].Codigo, [818_Certificados_Patologia].Descripcion, [818_Certificados_Patologia].Cantidad
    FROM [818_Certificados_Patologia](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 819_Insert_Certificados_AfeccionGrupo =====
-- DependsOn: 819_Certificados_AfeccionGrupo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_819_Insert_Certificados_AfeccionGrupo]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [819_Certificados_AfeccionGrupo].Codigo, [819_Certificados_AfeccionGrupo].Descrip, [819_Certificados_AfeccionGrupo].Cantidad
    FROM [819_Certificados_AfeccionGrupo](NULL, NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 820_Insert_Certificados_AfeccionTipo =====
-- DependsOn: 820_Certificados_AfeccionTipo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_820_Insert_Certificados_AfeccionTipo]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pCodPatologia NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [820_Certificados_AfeccionTipo].Codigo, [820_Certificados_AfeccionTipo].Descrip, [820_Certificados_AfeccionTipo].Cantidad
    FROM [820_Certificados_AfeccionTipo](NULL, NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_CertificadosCantidad =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_CertificadosCantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT GE.Descrip AS GE, Count(*) AS Cantidad
    FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
    WHERE (((A.CI) In (Select CI From Certificacion Where Efectiva = 1 And FechaCertificacion Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE FechaCertificacion END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE FechaCertificacion END))) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)))
    GROUP BY GE.Descrip;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosActivos_GE =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosActivos_GE]
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.CI, GE.Descrip AS DescGrupoEtario
    FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
    WHERE (((A.CI) In (Select CI From Trabaja Where 
    FechaIngreso <= (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END))
    )) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_AfiliadosActivos_GE2 =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_AfiliadosActivos_GE2]
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.CI, A.Edad, GrupoEtario.Descrip AS DescGrupoEtario
    FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario
    WHERE (((A.CI) In (Select CI From Trabaja Where 
    FechaIngreso <= (CASE WHEN @pFecha IS NULL THEN FechaIngreso ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN DATEADD(day,-1,FechaBaja) ELSE FechaBaja END))
    )) AND ((A.Edad) Between [GrupoEtario].[EdadIni] And (CASE WHEN [GrupoEtario].[EdadFin]=0 THEN [A].[Edad] ELSE [GrupoEtario].[EdadIni] END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 822_Insert_GEPatologia_Cantidad =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_822_Insert_GEPatologia_Cantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip2] ( Descrip, Descrip2, Cantidad )
    SELECT GE.Descrip, MIN(Patologia.Descrip) AS FirstOfDescrip, Count(*) AS Expr1
    FROM GrupoEtario AS GE, (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia) INNER JOIN [acc_sgpa_822_AfiliadoGE_q] AS A ON Certificacion.CI = A.CI
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN A.Edad ELSE GE.EdadIni END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN A.Edad ELSE GE.EdadFin END)))
    GROUP BY GE.Descrip, AfeccionGrupo.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 823_Insert_GE_Patologia_Cantidad =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_823_Insert_GE_Patologia_Cantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0),
    @pEdadIni NVARCHAR(MAX),
    @pEdadFin NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Descrip, Cantidad )
    SELECT MIN(Patologia.Descrip) AS FirstOfDescrip, Count(*) AS Expr1
    FROM (((Certificacion INNER JOIN AfeccionTipo ON Certificacion.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo) INNER JOIN AfeccionGrupo ON AfeccionTipo.CodAfeccionGrupo = AfeccionGrupo.CodAfeccionGrupo) INNER JOIN Patologia ON AfeccionGrupo.CodPatologia = Patologia.CodPatologia) INNER JOIN [acc_sgpa_822_AfiliadoGE_q] AS A ON Certificacion.CI = A.CI
    WHERE (((Certificacion.Efectiva)= 1) AND ((Certificacion.FechaCertificacion) Between (CASE WHEN @pFechaIni Is NOT Null THEN @pFechaIni ELSE [FechaCertificacion] END) And (CASE WHEN @pFechaFin Is NOT Null THEN @pFechaFin ELSE [FechaCertificacion] END)) AND ((A.Edad) >= (CASE WHEN @pEdadIni IS NULL THEN [A].[Edad] ELSE @pEdadIni END) And (A.Edad) <=(CASE WHEN @pEdadFin IS NULL THEN [A].[Edad] ELSE @pEdadFin END)))
    GROUP BY AfeccionGrupo.CodPatologia;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 827_AfiliadosActivos_GE_Sexo =====
-- DependsOn: 822_AfiliadoGE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_827_AfiliadosActivos_GE_Sexo]
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT A.CI, GE.Descrip AS DescGrupoEtario, A.Sexo
    FROM [acc_sgpa_822_AfiliadoGE_q] AS A, GrupoEtario AS GE
    WHERE (((A.CI) In (Select CI From Trabaja Where 
    FechaIngCasemed <= (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END) AND (FechaBaja Is Null Or FechaBaja > (CASE WHEN @pFecha IS NULL THEN CAST(GETDATE() AS date) ELSE @pFecha END))
    )) AND ((A.Edad)>=(CASE WHEN [GE].[EdadIni]=0 THEN [A].[Edad] ELSE [GE].[EdadIni] END) And (A.Edad)<=(CASE WHEN [GE].[EdadFin]=0 THEN [A].[Edad] ELSE [GE].[EdadFin] END)));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 824_Insert_PrestacionesCantidad =====
-- DependsOn: 824_PrestacionesCantidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_824_Insert_PrestacionesCantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [824_PrestacionesCantidad].CodPrestacionTipo, [824_PrestacionesCantidad].DescPrestacionTipo, [824_PrestacionesCantidad].Cantidad
    FROM [824_PrestacionesCantidad](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 825_Insert_PrestacionesImporte_Pesos =====
-- DependsOn: 825_PrestacionesImporte_Pesos
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_825_Insert_PrestacionesImporte_Pesos]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [825_PrestacionesImporte_Pesos].CodPrestacionTipo, [825_PrestacionesImporte_Pesos].DescPrestacionTipo, [825_PrestacionesImporte_Pesos].Importe
    FROM [825_PrestacionesImporte_Pesos](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 826_Insert_PrestacionesImporte_Dolares =====
-- DependsOn: 826_PrestacionesImporte_Dolares
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_826_Insert_PrestacionesImporte_Dolares]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [826_PrestacionesImporte_Dolares].CodPrestacionTipo, [826_PrestacionesImporte_Dolares].DescPrestacionTipo, [826_PrestacionesImporte_Dolares].Importe
    FROM [826_PrestacionesImporte_Dolares](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 830_Rpt_Cantidad_Por_Puesto =====
-- DependsOn: 830_CantidadPorPuesto, 830_CantidadPorPuestoNo0
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_830_Rpt_Cantidad_Por_Puesto]
    @pFecha DATETIME2(0),
    @pMes INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.CodEmpresa, Empresa.Nombre, [830_CantidadPorPuesto].Cantidad, [830_CantidadPorPuestoNo0].Cantidad AS CantidadNo0
    FROM (Empresa LEFT JOIN [acc_sgpa_830_CantidadPorPuesto_q](@pFecha) AS [830_CantidadPorPuesto] ON Empresa.CodEmpresa = [830_CantidadPorPuesto].CodEmpresa) LEFT JOIN [acc_sgpa_830_CantidadPorPuestoNo0_q](@pFecha, @pMes) AS [830_CantidadPorPuestoNo0] ON Empresa.CodEmpresa = [830_CantidadPorPuestoNo0].CodEmpresa
    WHERE (((Empresa.Ficticia)= 0));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Afiliado_Tmp =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Afiliado_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN LEN(acc_sgpa_Rpt_Afiliado_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento, acc_sgpa_Rpt_Afiliado_q.Movil
    FROM [acc_sgpa_Rpt_Afiliado_q]
    WHERE ( acc_sgpa_Rpt_Afiliado_q.CI IN (SELECT CI FROM Trabaja Where FechaBaja Between TRY_CONVERT(datetime2,'01/05/2014') And TRY_CONVERT(datetime2,'31/05/2014') And CodBajaMotivo IN (5,12,22,3)) )
    ORDER BY [CI];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_AfiliadoFormatCI =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_AfiliadoFormatCI]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN LEN(acc_sgpa_Rpt_Afiliado_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Afiliado_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento, acc_sgpa_Rpt_Afiliado_q.Movil
    FROM [acc_sgpa_Rpt_Afiliado_q] AS acc_sgpa_Rpt_Afiliado_q;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_AfiliadoNOFormatCI =====
-- DependsOn: Rpt_Afiliado
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_AfiliadoNOFormatCI]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT CONVERT(nvarchar(max),acc_sgpa_Rpt_Afiliado_q.CI) AS CI, acc_sgpa_Rpt_Afiliado_q.Nombres, acc_sgpa_Rpt_Afiliado_q.Apellido1, acc_sgpa_Rpt_Afiliado_q.Apellido2, acc_sgpa_Rpt_Afiliado_q.FechaNacimiento, acc_sgpa_Rpt_Afiliado_q.Sexo, acc_sgpa_Rpt_Afiliado_q.Telefono, acc_sgpa_Rpt_Afiliado_q.EMail, acc_sgpa_Rpt_Afiliado_q.CodMutualista, acc_sgpa_Rpt_Afiliado_q.DescMutualista, acc_sgpa_Rpt_Afiliado_q.FechaIngMutualista, acc_sgpa_Rpt_Afiliado_q.NroSocioMutualista, acc_sgpa_Rpt_Afiliado_q.CodRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.DescRegimenJubilatorio, acc_sgpa_Rpt_Afiliado_q.Usr, acc_sgpa_Rpt_Afiliado_q.Ts, acc_sgpa_Rpt_Afiliado_q.DescAfiliado, acc_sgpa_Rpt_Afiliado_q.Cuota, acc_sgpa_Rpt_Afiliado_q.Direccion, acc_sgpa_Rpt_Afiliado_q.PagaMutualista, acc_sgpa_Rpt_Afiliado_q.CodDepartamento, acc_sgpa_Rpt_Afiliado_q.DescDepartamento
    FROM [acc_sgpa_Rpt_Afiliado_q] AS acc_sgpa_Rpt_Afiliado_q;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Certificado_Tmp =====
-- DependsOn: Rpt_Certificacion
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Certificado_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_Rpt_Certificacion_q.NroLlamado, (CASE WHEN LEN(acc_sgpa_Rpt_Certificacion_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_Certificacion_q.NroRecibo, acc_sgpa_Rpt_Certificacion_q.FechaRecibido, acc_sgpa_Rpt_Certificacion_q.FechaCertificacion, acc_sgpa_Rpt_Certificacion_q.FechaIni, acc_sgpa_Rpt_Certificacion_q.FechaFin, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.CodCertificador, acc_sgpa_Rpt_Certificacion_q.DescCertificador, acc_sgpa_Rpt_Certificacion_q.CodSalidaTipo, acc_sgpa_Rpt_Certificacion_q.DescSalidaTipo, acc_sgpa_Rpt_Certificacion_q.Efectiva, acc_sgpa_Rpt_Certificacion_q.Indicaciones, acc_sgpa_Rpt_Certificacion_q.ImporteDeducible, acc_sgpa_Rpt_Certificacion_q.Usr, acc_sgpa_Rpt_Certificacion_q.Ts, acc_sgpa_Rpt_Certificacion_q.Nombres, acc_sgpa_Rpt_Certificacion_q.Apellido1, acc_sgpa_Rpt_Certificacion_q.Apellido2, acc_sgpa_Rpt_Certificacion_q.DescAfiliado
    FROM [acc_sgpa_Rpt_Certificacion_q]
    WHERE [NroLlamado] = 4240;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_DiasCertificacion_Tmp =====
-- DependsOn: Rpt_Certificacion
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_DiasCertificacion_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_Rpt_Certificacion_q.CI, (CASE WHEN LEN(acc_sgpa_Rpt_Certificacion_q.CI)>=8 THEN FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_Certificacion_q.CI,'@@@\.@@@-@') END) AS CIFormat, MIN(acc_sgpa_Rpt_Certificacion_q.DescAfiliado) AS DescAfiliado, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, MIN(acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo) AS DescAfeccionTipo, Sum(DATEDIFF(day,[FechaIni],[FechaFin])+1) AS Dias, Count(*) AS Cantidad
    FROM [acc_sgpa_Rpt_Certificacion_q]
    GROUP BY acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_Mutualista_Tmp =====
-- DependsOn: Rpt_Mutualista
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_Mutualista_Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sgpa_Rpt_Mutualista_q];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 250_AfiliadoConDerecho =====
-- DependsOn: rs_AfiliadoImponibleMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_250_AfiliadoConDerecho]
    @pMesFin INT,
    @pMesIni INT,
    @pSMN INT
AS
BEGIN
    SET NOCOUNT ON;
    select ai.ci FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q] as ai
    where ai.aniomes between @pMesIni and @pMesFin
    group by ai.ci
    having (sum(ai.importe)/6) >= (1.25 * @pSMN)
    UNION select ai.ci FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q] as ai
    where ai.aniomes = @pMesFin
    and ai.importe >= (1.25 * @pSMN)
    group by ai.ci;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Promedio =====
-- DependsOn: Rs_AfiliadoImponibleMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Promedio]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_Rs_AfiliadoImponibleMes_q.CI, Avg(acc_sgpa_Rs_AfiliadoImponibleMes_q.Importe) AS Promedio
    FROM [acc_sgpa_Rs_AfiliadoImponibleMes_q]
    WHERE (((acc_sgpa_Rs_AfiliadoImponibleMes_q.AnioMes) Between (YEAR(DATEADD(month,-7,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-7,CAST(GETDATE() AS date)))) And (YEAR(DATEADD(month,-2,CAST(GETDATE() AS date))) * 100 + MONTH(DATEADD(month,-2,CAST(GETDATE() AS date))))))
    GROUP BY acc_sgpa_Rs_AfiliadoImponibleMes_q.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 320_AfiliadoPromedio =====
-- DependsOn: Rs_AfiliadoImponibleMesNoBaja
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_320_AfiliadoPromedio]
    @pCI INT,
    @pMesAnioIni INT,
    @pMesAnioFin INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Avg(acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.Importe) AS Importe
    FROM [acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]
    WHERE (((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.CI)=@pCI) AND ((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.AnioMes) Between @pMesAnioIni And @pMesAnioFin));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 320_AfiliadoUltMes =====
-- DependsOn: Rs_AfiliadoImponibleMesNoBaja
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_320_AfiliadoUltMes]
    @pCI INT,
    @pMesAnio INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Avg(acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.Importe) AS Importe
    FROM [acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q]
    WHERE (((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.CI)=@pCI) AND ((acc_sgpa_Rs_AfiliadoImponibleMesNoBaja_q.AnioMes)=@pMesAnio));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Insert_NoCargadoHL =====
-- DependsOn: Rs_Bps2, 100_CargadosHL
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Insert_NoCargadoHL]
    @pMes TINYINT,
    @pAnio INT,
    @pCodEmpresa INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO NoCargadoHL ( CI, Apellido1, Apellido2, Nombres, CodEmpresa, Mes, Anio, Usr, Ts )
    SELECT acc_sgpa_Rs_Bps2_q.Cedula, MIN(acc_sgpa_Rs_Bps2_q.Apellido1) AS PrimeroDeApellido1, MIN(acc_sgpa_Rs_Bps2_q.Apellido2) AS PrimeroDeApellido2, MIN(acc_sgpa_Rs_Bps2_q.Nombres) AS PrimeroDeNombres, @pCodEmpresa AS Expr1, @pMes AS Expr2, @pAnio AS Expr3, @pUsr AS Expr4, SYSDATETIME() AS Expr5
    FROM [acc_sgpa_Rs_Bps2_q] LEFT JOIN [100_CargadosHL](@pMes, @pAnio, @pCodEmpresa) ON acc_sgpa_Rs_Bps2_q.Cedula = [100_CargadosHL].CI
    WHERE ((([100_CargadosHL].CI) Is Null))
    GROUP BY acc_sgpa_Rs_Bps2_q.Cedula;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Create_Bps4Tmp =====
-- DependsOn: Rs_Bps4
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Create_Bps4Tmp]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TRY_CONVERT(float,Rs_Bps4.Cedula) AS CI, Rs_Bps4.Concepto, Rs_Bps4.Imponible
    FROM [acc_sgpa_Rs_Bps4_q] AS Rs_Bps4;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Certificacion =====
-- DependsOn: Rs_Certificacion_Nombre
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Certificacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sgpa_Rs_Certificacion_Nombre_q]
    WHERE ( acc_sgpa_Rs_Certificacion_Nombre_q.[CI] = 11391501 );
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 510_Rpt_Trabaja =====
-- DependsOn: Rs_Imponible_Ult
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_510_Rpt_Trabaja]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, acc_sgpa_Rs_Imponible_Ult_q.Importe
    FROM ((Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN [acc_sgpa_Rs_Imponible_Ult_q] ON (Trabaja.CI = acc_sgpa_Rs_Imponible_Ult_q.CI) AND (Trabaja.CodEmpresa = acc_sgpa_Rs_Imponible_Ult_q.CodEmpresa)
    WHERE ( [Trabaja].[CodEmpresa] = 1 );
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_Trabaja_DBG =====
-- DependsOn: Rs_Imponible_Ult
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_Trabaja_DBG]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.CI, (CASE WHEN LEN(Afiliado.CI)>=8 THEN FORMAT(Afiliado.CI,'@\.@@@\.@@@-@') ELSE FORMAT(Afiliado.CI,'@@@\.@@@-@') END) AS CIFormat, Afiliado.Nombres, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.FechaNacimiento, Afiliado.Sexo, Afiliado.Direccion, Afiliado.Telefono, Afiliado.EMail, Afiliado.CodMutualista, Afiliado.FechaIngMutualista, Afiliado.NroSocioMutualista, Afiliado.CodRegimenJubilatorio, Afiliado.CodDepartamento, Afiliado.PagaMutualista, Afiliado.Usr, Afiliado.Ts, Trabaja.CodEmpresa, Empresa.Nombre AS DescEmpresa, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, Trabaja.NroFichaEmpresa, acc_sgpa_Rs_Imponible_Ult_q.Importe
    FROM ((Afiliado INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) LEFT JOIN [acc_sgpa_Rs_Imponible_Ult_q] ON (Trabaja.CI = acc_sgpa_Rs_Imponible_Ult_q.CI) AND (Trabaja.CodEmpresa = acc_sgpa_Rs_Imponible_Ult_q.CodEmpresa)
    WHERE (((Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 150_5_Mejores_Pagos =====
-- DependsOn: Rs_MaxImp_Afiliado
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_150_5_Mejores_Pagos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5 Afiliado.CI, I.Mes, I.Anio, Afiliado.Apellido1, Afiliado.Apellido2, Afiliado.Nombres, Empresa.Nombre, I.Importe
    FROM Empresa INNER JOIN ((SELECT Imponible.CI, MIN(Imponible.CodEmpresa) AS CodEmpresa, MIN(Imponible.Mes) AS Mes, MIN(Imponible.Anio) AS Anio, Max(Imponible.Importe) AS Importe FROM Imponible GROUP BY Imponible.CI) AS I INNER JOIN Afiliado ON I.CI = Afiliado.CI) ON Empresa.CodEmpresa = I.CodEmpresa
    ORDER BY I.Importe DESC;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: acc_sgpa_300_SubsidioItemCod_Full_Data_q =====
-- DependsOn: Rs_SubsidioItemCodXCI
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_acc_sgpa_300_SubsidioItemCod_Full_Data_q]
    @pFecha DATETIME2(0),
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SubsidioItemCod.CodSubsidioItemCod, SubsidioItemCod.Descrip, SubsidioItemCod.Tipo, SubsidioItemCod.ValorTipo, SubsidioItemCod.Signo, SubsidioItemCod.Comparar, SubsidioItemCod.CompararContra, SubsidioItemCod.Valor, SubsidioItemCod.TipoComp, SubsidioItemCod.Operador, SubsidioItemCod.ValorMin, SubsidioItemCod.ValorMax, SubsidioItemCod.Procesar, SubsidioItemCod.FechaVigencia, SubsidioItemCod.FechaBaja, SubsidioItemCod.ModificaNominal, 0 As CI
    FROM SubsidioItemCod
    WHERE (((SubsidioItemCod.Procesar)= 1) AND ((SubsidioItemCod.FechaVigencia)<=@pFecha) AND ((SubsidioItemCod.FechaBaja)>@pFecha Or (SubsidioItemCod.FechaBaja) Is Null)) 
    UNION ALL SELECT *
    FROM [acc_sgpa_Rs_SubsidioItemCodXCI_q]
    WHERE (((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaVigencia)<=@pFecha) AND ((acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja)>@pFecha Or (acc_sgpa_Rs_SubsidioItemCodXCI_q.FechaBaja) Is Null)) and CI = @pCI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Update_Imponible =====
-- DependsOn: Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Update_Imponible]
    @pCodEmpresa INT,
    @pMes INT,
    @pAno INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Imponible SET Imponible.Importe = [Imponible].[Importe]+[Bps4Tmp].[Imponible], Imponible.Usr = @pUsr, Imponible.Ts = SYSDATETIME() FROM Bps4Tmp INNER JOIN (Rs_TrabajaActivo INNER JOIN Imponible ON (acc_sgpa_Rs_TrabajaActivo_q.CI = Imponible.CI) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa = Imponible.CodEmpresa) AND (acc_sgpa_Rs_TrabajaActivo_q.FechaIngreso = Imponible.Fechaingreso)) ON Bps4Tmp.CI = acc_sgpa_Rs_TrabajaActivo_q.CI
    WHERE (((acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa)=@pCodEmpresa) AND ((Imponible.Mes)=@pMes) AND ((Imponible.Anio)=@pAno));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_CertificadoEmpresa_S =====
-- DependsOn: Rpt_Certificacion, Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa_S]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_Rpt_Certificacion_q.NroLlamado, acc_sgpa_Rpt_Certificacion_q.CI, acc_sgpa_Rpt_Certificacion_q.NroRecibo, acc_sgpa_Rpt_Certificacion_q.FechaRecibido, acc_sgpa_Rpt_Certificacion_q.FechaCertificacion, acc_sgpa_Rpt_Certificacion_q.FechaIni, acc_sgpa_Rpt_Certificacion_q.FechaFin, acc_sgpa_Rpt_Certificacion_q.CodAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.DescAfeccionTipo, acc_sgpa_Rpt_Certificacion_q.CodCertificador, acc_sgpa_Rpt_Certificacion_q.DescCertificador, acc_sgpa_Rpt_Certificacion_q.CodSalidaTipo, acc_sgpa_Rpt_Certificacion_q.DescSalidaTipo, acc_sgpa_Rpt_Certificacion_q.Efectiva, acc_sgpa_Rpt_Certificacion_q.Indicaciones, acc_sgpa_Rpt_Certificacion_q.ImporteDeducible, acc_sgpa_Rpt_Certificacion_q.Usr, acc_sgpa_Rpt_Certificacion_q.Ts, acc_sgpa_Rpt_Certificacion_q.Nombres, acc_sgpa_Rpt_Certificacion_q.Apellido1, acc_sgpa_Rpt_Certificacion_q.Apellido2, acc_sgpa_Rpt_Certificacion_q.DescAfiliado, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, Empresa.Nombre AS DescEmpresa
    FROM ([acc_sgpa_Rpt_Certificacion_q] AS acc_sgpa_Rpt_Certificacion_q INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON acc_sgpa_Rpt_Certificacion_q.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) INNER JOIN Empresa ON acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 505_Insert_SubsidioEnfermedadMes =====
-- DependsOn: Rs_SubsidioXMes, Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_505_Insert_SubsidioEnfermedadMes]
    @pMes INT,
    @pAno INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Imponible ( CI, DiasTrabajados, CodEmpresa, Importe, Anio, Mes, IdTrabaja, Fechaingreso, Concepto, AnioMes )
    SELECT acc_sgpa_Rs_SubsidioXMes_q.CI, 30 AS Expr2, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, acc_sgpa_Rs_SubsidioXMes_q.ImpNominal, acc_sgpa_Rs_SubsidioXMes_q.Anio, acc_sgpa_Rs_SubsidioXMes_q.Mes, acc_sgpa_Rs_TrabajaActivo_q.IdTrabaja, acc_sgpa_Rs_TrabajaActivo_q.FechaIngreso, '1' AS Expr1, (acc_sgpa_Rs_SubsidioXMes_q.Anio*100)+acc_sgpa_Rs_SubsidioXMes_q.Mes AS AnioMes
    FROM [acc_sgpa_Rs_SubsidioXMes_q] INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON acc_sgpa_Rs_SubsidioXMes_q.CI = acc_sgpa_Rs_TrabajaActivo_q.CI
    WHERE (((acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa)=900) AND ((acc_sgpa_Rs_SubsidioXMes_q.Anio)=@pAno) AND ((acc_sgpa_Rs_SubsidioXMes_q.Mes)=@pMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 505_Temporal =====
-- DependsOn: Rs_SubsidioXMes, Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_505_Temporal]
    @pMes NVARCHAR(MAX),
    @pAno NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_Rs_TrabajaActivo_q.IdTrabaja, '1' AS Expr1, 30 AS Expr2, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, acc_sgpa_Rs_SubsidioXMes_q.ImpNominal, acc_sgpa_Rs_SubsidioXMes_q.Anio, acc_sgpa_Rs_SubsidioXMes_q.Mes
    FROM [acc_sgpa_Rs_SubsidioXMes_q] INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON acc_sgpa_Rs_SubsidioXMes_q.CI = acc_sgpa_Rs_TrabajaActivo_q.CI
    WHERE (((acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa)=900) AND ((acc_sgpa_Rs_SubsidioXMes_q.Anio)=@pAno) AND ((acc_sgpa_Rs_SubsidioXMes_q.Mes)=@pMes));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_Promedio_Ult6_Todos =====
-- DependsOn: 800_AfiliadoImponible_Mes, Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_Promedio_Ult6_Todos]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT I.CI, Avg(I.Importe) AS Promedio
    FROM [acc_sgpa_800_AfiliadoImponible_Mes_q](@pCodEmpresa) AS I INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON (I.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) AND (I.CI = acc_sgpa_Rs_TrabajaActivo_q.CI)
    WHERE ((TRY_CONVERT(float,CONVERT(nvarchar(max),[I].[Anio]) + RIGHT('00' + CONVERT(varchar(2), [I].[Mes]), 2)) Between @pMesIni And @pMes) AND (acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE acc_sgpa_Rs_TrabajaActivo_q.[CodEmpresa] END)))
    GROUP BY I.CI
    HAVING (Avg([I].[Importe])=0);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Insert_Imponible =====
-- DependsOn: Rs_Bps4, Rs_TrabajaUltimo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Insert_Imponible]
    @pCodEmpresa INT,
    @pMes NVARCHAR(MAX),
    @pAno INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Imponible ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, DiasTrabajados, Usr, Ts, Fechaingreso, IdTrabaja, AnioMes )
    SELECT Rs_Bps4.Cedula, Rs_Bps4.Concepto, Sum(Rs_Bps4.Imponible) AS SumOfImporte, @pCodEmpresa AS Expr1, @pMes AS Expr2, @pAno AS Expr3, 30 AS DiasTrabajados, @pUsr AS Usuario, SYSDATETIME() AS Rs, MIN(T.FechaIngreso) AS PrimeroDeFechaIngreso, MIN(T.IdTrabaja) AS PrimeroDeIdRs_TrabajaActivo, TRY_CONVERT(float,CONVERT(nvarchar(max),@pAno) + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AS Expr4
    FROM (Rs_Bps4 INNER JOIN Afiliado ON Rs_Bps4.Cedula = Afiliado.CI) INNER JOIN [acc_sgpa_Rs_TrabajaUltimo_q] AS T ON Afiliado.CI = T.CI
    WHERE (((T.CodEmpresa)=@pCodEmpresa))
    GROUP BY Rs_Bps4.Cedula, Rs_Bps4.Concepto;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 100_Insert_Imponible_New =====
-- DependsOn: Rs_Bps4, Rs_TrabajaUltimo, 101_ImponibleMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_100_Insert_Imponible_New]
    @pCodEmpresa INT,
    @pMes INT,
    @pAno INT,
    @pUsr NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Imponible ( CI, Concepto, Importe, CodEmpresa, Mes, Anio, Fechaingreso, IdTrabaja, DiasTrabajados, Usr, Ts, AnioMes )
    SELECT Rs_Bps4.Cedula, Rs_Bps4.Concepto, Sum(Rs_Bps4.Imponible) AS SumOfImporte, @pCodEmpresa AS Expr1, @pMes AS Expr2, @pAno AS Expr3, MIN(t.FechaIngreso) AS FirstOfFechaIngreso, MIN(t.IdTrabaja) AS FirstOfIdRs_TrabajaActivo, 30 AS Expr5, @pUsr AS Expr6, SYSDATETIME() AS Expr7, TRY_CONVERT(float,CONVERT(nvarchar(max),@pAno) + RIGHT('00' + CONVERT(varchar(2), @pMes), 2)) AS Expr4
    FROM (([acc_sgpa_Rs_Bps4_q] AS Rs_Bps4 INNER JOIN Afiliado ON Rs_Bps4.Cedula = Afiliado.CI) INNER JOIN [acc_sgpa_Rs_TrabajaUltimo_q] AS t ON Afiliado.CI = t.CI) LEFT JOIN [acc_sgpa_101_ImponibleMes_q](@pMes, @pAno) AS I ON (t.CI = I.CI) AND (t.CodEmpresa = I.CodEmpresa) AND (t.FechaIngreso = I.Fechaingreso)
    WHERE (((t.CodEmpresa)=@pCodEmpresa) AND ((I.CI) Is Null))
    GROUP BY Rs_Bps4.Cedula, Rs_Bps4.Concepto, Rs_Bps4.Concepto;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xRptEmpresaCantidad =====
-- DependsOn: xEmpresaCantidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xRptEmpresaCantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.Nombre, acc_sgpa_xEmpresaCantidad_q.Cantidad
    FROM [acc_sgpa_xEmpresaCantidad_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaCantidad_q.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xRptPromedioEmpresa =====
-- DependsOn: xEmpresaPromedio
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xRptPromedioEmpresa]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.Nombre, FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedio_q.PromedioDeImporte), '0.00') AS Promedio
    FROM [acc_sgpa_xEmpresaPromedio_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedio_q.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xRptEmpresaPromedioTodo =====
-- DependsOn: xEmpresaPromedioTodo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xRptEmpresaPromedioTodo]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.Nombre, FORMAT(TRY_CONVERT(decimal(18,2),acc_sgpa_xEmpresaPromedioTodo_q.PromedioDeImporte),'0.00') AS Promedio
    FROM [acc_sgpa_xEmpresaPromedioTodo_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedioTodo_q.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xRptMutualistaCantidad =====
-- DependsOn: xMutualistaCantidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xRptMutualistaCantidad]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Mutualista.Descrip, acc_sgpa_xMutualistaCantidad_q.Cantidad
    FROM [acc_sgpa_xMutualistaCantidad_q] INNER JOIN Mutualista ON acc_sgpa_xMutualistaCantidad_q.CodMutualista = Mutualista.CodMutualista;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xEmpresaPromedioNo0 =====
-- DependsOn: xSinImponiblexEmpresa
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xEmpresaPromedioNo0]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Imponible.CodEmpresa, FORMAT(Avg(Imponible.Importe), '0.00') AS PromedioDeImporte
    FROM Imponible LEFT JOIN [acc_sgpa_xSinImponiblexEmpresa_q] ON (Imponible.CodEmpresa = acc_sgpa_xSinImponiblexEmpresa_q.CodEmpresa) AND (Imponible.CI = acc_sgpa_xSinImponiblexEmpresa_q.CI)
    WHERE (((TRY_CONVERT(float,[Anio] + RIGHT('00' + CONVERT(varchar(2), [Mes]), 2))) Between 199906 And 199911) AND ((acc_sgpa_xSinImponiblexEmpresa_q.CI) Is Null))
    GROUP BY Imponible.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_PagarMutualista =====
-- DependsOn: 200_Imponible_6_Meses, 200_Imponible_Ult_Mes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_PagarMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, SituacionMutual.Pagar
    FROM (Afiliado INNER JOIN [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN) AS I ON Afiliado.CI = I.CI) INNER JOIN SituacionMutual ON Afiliado.CodSituacionMutual = SituacionMutual.CodSituacionMutual
    WHERE (((Afiliado.CodMutualista)>0) AND ((SituacionMutual.Pagar)= 1))
    UNION SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, SituacionMutual.Pagar
    FROM (Afiliado INNER JOIN [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN) AS I ON Afiliado.CI = I.CI) INNER JOIN SituacionMutual ON Afiliado.CodSituacionMutual = SituacionMutual.CodSituacionMutual
    WHERE (((Afiliado.CodMutualista)>0) AND ((SituacionMutual.Pagar)= 1))
    ORDER BY CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 201_PagarMutualista =====
-- DependsOn: 200_Imponible_6_Meses, 200_Imponible_Ult_Mes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_201_PagarMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN)
    UNION SELECT * FROM [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_AfiliadoValorJornal =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_AfiliadoValorJornal]
    @pCodCasemed INT,
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [300_AfiliadoDiasImporte].CI, Sum([300_AfiliadoDiasImporte].Promedio) AS Promedio
    FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
    WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)<>@pCodCasemed))
    GROUP BY [300_AfiliadoDiasImporte].CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_AfiliadoValorJornalCasemed =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_AfiliadoValorJornalCasemed]
    @pCodCasemed INT,
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [300_AfiliadoDiasImporte].CI, Sum(([Importe]/180)) AS Promedio
    FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
    WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)=@pCodCasemed))
    GROUP BY [300_AfiliadoDiasImporte].CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_AfiliadoValorJornalxEmpresa =====
-- DependsOn: 300_AfiliadoDiasImporte
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_AfiliadoValorJornalxEmpresa]
    @pCodCasemed INT,
    @pCI INT,
    @pMesIni INT,
    @pMesFin INT,
    @pLiquidar NVARCHAR(MAX),
    @pDias NVARCHAR(MAX),
    @pMes NVARCHAR(MAX),
    @pMesIniImp INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [300_AfiliadoDiasImporte].CI, [300_AfiliadoDiasImporte].CodEmpresa, Sum([300_AfiliadoDiasImporte].Promedio) AS Promedio
    FROM [acc_sgpa_300_AfiliadoDiasImporte_q](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp) AS [300_AfiliadoDiasImporte]
    WHERE ((([300_AfiliadoDiasImporte].CodEmpresa)<>@pCodCasemed))
    GROUP BY [300_AfiliadoDiasImporte].CI, [300_AfiliadoDiasImporte].CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Promedio_Gral =====
-- DependsOn: 400_Promedio_Mes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Promedio_Gral]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Avg(acc_sgpa_400_Promedio_Mes_q.Importe) AS PromedioDeImporte
    FROM [acc_sgpa_400_Promedio_Mes_q] AS acc_sgpa_400_Promedio_Mes_q;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 400_Promedio_Gral_Puesto =====
-- DependsOn: 400_Promedio_Mes_Puesto
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_400_Promedio_Gral_Puesto]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Avg(acc_sgpa_400_Promedio_Mes_Puesto_q.Importe) AS PromedioDeImporte
    FROM [acc_sgpa_400_Promedio_Mes_Puesto_q] AS acc_sgpa_400_Promedio_Mes_Puesto_q;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Update_Prorroga =====
-- DependsOn: 480_Prorrogas
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Update_Prorroga]
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE A SET A.DiaProrroga = acc_sgpa_480_Prorrogas_q.[Dias], A.F_Ult_Prorroga = acc_sgpa_480_Prorrogas_q.[F_Ult_Prorroga] FROM [600_Afiliado_Certificado] AS A INNER JOIN [acc_sgpa_480_Prorrogas_q] ON A.CI = acc_sgpa_480_Prorrogas_q.CI
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Insert_Rpt_ResumenSubsidio_Tmp =====
-- DependsOn: 500_Rpt_ResumenSubsidio, 756_NoBaja
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Insert_Rpt_ResumenSubsidio_Tmp]
    @pMes NVARCHAR(MAX),
    @pAnio NVARCHAR(MAX),
    @pLiquidar NVARCHAR(MAX),
    @pFecha DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_SubsidioResumen_Tmp] ( Baja, Mes, Anio, Liquidar, CI, DescAfiliado, Dias, Nombres, Apellido1, Apellido2, ImpNominal, ImpAguinaldo, ImpLiquido, FechaNacimiento, DescFecha, CIOrig )
    SELECT (CASE WHEN [756_NoBaja].CI IS NULL THEN 1 ELSE 0 END) AS Baja, acc_sgpa_500_Rpt_ResumenSubsidio_q.Mes, acc_sgpa_500_Rpt_ResumenSubsidio_q.Anio, acc_sgpa_500_Rpt_ResumenSubsidio_q.Liquidar, acc_sgpa_500_Rpt_ResumenSubsidio_q.CI, acc_sgpa_500_Rpt_ResumenSubsidio_q.DescAfiliado, acc_sgpa_500_Rpt_ResumenSubsidio_q.Dias, acc_sgpa_500_Rpt_ResumenSubsidio_q.Nombres, acc_sgpa_500_Rpt_ResumenSubsidio_q.Apellido1, acc_sgpa_500_Rpt_ResumenSubsidio_q.Apellido2, acc_sgpa_500_Rpt_ResumenSubsidio_q.ImpNominal, acc_sgpa_500_Rpt_ResumenSubsidio_q.ImpAguinaldo, acc_sgpa_500_Rpt_ResumenSubsidio_q.ImpLiquido, acc_sgpa_500_Rpt_ResumenSubsidio_q.FechaNacimiento, acc_sgpa_500_Rpt_ResumenSubsidio_q.DescFecha, acc_sgpa_500_Rpt_ResumenSubsidio_q.CIOrig
    FROM [acc_sgpa_500_Rpt_ResumenSubsidio_q] LEFT JOIN [756_NoBaja](@pFecha) AS [756_NoBaja] ON acc_sgpa_500_Rpt_ResumenSubsidio_q.CIOrig = [756_NoBaja].CI
    WHERE (((acc_sgpa_500_Rpt_ResumenSubsidio_q.Mes)=@pMes) AND ((acc_sgpa_500_Rpt_ResumenSubsidio_q.Anio)=@pAnio) AND ((acc_sgpa_500_Rpt_ResumenSubsidio_q.Liquidar)=@pLiquidar));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 815_Insert_AfiliadoEspecialidad =====
-- DependsOn: 815_AfiliadoImponible
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_815_Insert_AfiliadoEspecialidad]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_<125_Pct] ( Grupo, CI, Importe, Nombre )
    SELECT (CASE WHEN Especialidad.Descrip + '' <> '' THEN Especialidad.Descrip ELSE '(Sin Especialidad)' END) AS Grupo, [815_AfiliadoImponible].CI, [815_AfiliadoImponible].Importe, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM (([815_AfiliadoImponible](@pMes, @pSMN, @pCodEmpresa) INNER JOIN Afiliado ON [815_AfiliadoImponible].CI = Afiliado.CI) LEFT JOIN AfiliadoEspecialidad ON Afiliado.CI = AfiliadoEspecialidad.CI) LEFT JOIN Especialidad ON AfiliadoEspecialidad.CodEspecialidad = Especialidad.CodEspecialidad;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_Cantidad_Todos =====
-- DependsOn: 801_CI_Todos
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_Cantidad_Todos]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_801_CI_Todos_q](@pMes, @pMesIni, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_Ult6_Cantidad =====
-- DependsOn: 801_Promedio_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_Ult6_Cantidad]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_801_Promedio_Ult6_q](@pMes, @pMesIni, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 801_UltMes_Cantidad =====
-- DependsOn: 801_Promedio_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_801_UltMes_Cantidad]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_801_Promedio_UltMes_q](@pMes, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>0_Cantidad_Ult6 =====
-- DependsOn: 802_>0_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__0_Cantidad_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_802__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>0_Cantidad_UltMes =====
-- DependsOn: 802_>0_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__0_Cantidad_UltMes]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_802__0_UltMes_q](@pMes, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>125_Cantidad_Ult6 =====
-- DependsOn: 802_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__125_Cantidad_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_802_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 802_>125_Cantidad_UltMes =====
-- DependsOn: 802_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_802__125_Cantidad_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_802_UltMes_q](@pMes, @pSMN, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 803_>20_Cantidad_Ult6 =====
-- DependsOn: 803_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_803__20_Cantidad_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_803_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 803_>20_Cantidad_UltMes =====
-- DependsOn: 803_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_803__20_Cantidad_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count(*) AS Cantidad
    FROM [acc_sgpa_803_UltMes_q](@pMes, @pSMN, @pCodEmpresa);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>0_Masa_Ult6 =====
-- DependsOn: 804_>0_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__0_Masa_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum([804_>0_Ult6].Promedio) AS Masa
    FROM [acc_sgpa_804__0_Ult6_q](@pMes, @pMesIni, @pCodEmpresa) AS [804_>0_Ult6];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>0_Masa_UltMes =====
-- DependsOn: 804_>0_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__0_Masa_UltMes]
    @pMes INT,
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum([804_>0_UltMes].Promedio) AS Masa
    FROM [acc_sgpa_804__0_UltMes_q](@pMes, @pCodEmpresa) AS [804_>0_UltMes];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>20_Masa_Ult6 =====
-- DependsOn: 804_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__20_Masa_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum([804_Ult6].Promedio) AS Masa
    FROM [acc_sgpa_804_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [804_Ult6];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 804_>20_Masa_UltMes =====
-- DependsOn: 804_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_804__20_Masa_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Sum([804_UltMes].Promedio) AS Masa
    FROM [acc_sgpa_804_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [804_UltMes];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_AfiliadoCantidad =====
-- DependsOn: 811_Afiliado<125_Pct_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_AfiliadoCantidad]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Count([811_Afiliado<125_Pct_Ult6].CI) AS CountOfCI
    FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_Insert_Afiliados<125_Ult6 =====
-- DependsOn: 811_Afiliado<125_Pct_Ult6
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_Insert_Afiliados_125_Ult6]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_<125_Pct] ( CI, Nombre, Grupo, Importe )
    SELECT [811_Afiliado<125_Pct_Ult6].CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, [811_Afiliado<125_Pct_Ult6].Grupo, [811_Afiliado<125_Pct_Ult6].Importe
    FROM [acc_sgpa_811_Afiliado_125_Pct_Ult6_q](@pMes, @pMesIni, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_Ult6] INNER JOIN Afiliado ON [811_Afiliado<125_Pct_Ult6].CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 811_Insert_Afiliados<125_UltMes =====
-- DependsOn: 811_Afiliado<125_Pct_UltMes
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_811_Insert_Afiliados_125_UltMes]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_<125_Pct] ( CI, Nombre, Grupo, Importe )
    SELECT [811_Afiliado<125_Pct_UltMes].CI, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado, [811_Afiliado<125_Pct_UltMes].Grupo, [811_Afiliado<125_Pct_UltMes].Importe
    FROM [acc_sgpa_811_Afiliado_125_Pct_UltMes_q](@pMes, @pSMN, @pCodEmpresa) AS [811_Afiliado<125_Pct_UltMes] INNER JOIN Afiliado ON [811_Afiliado<125_Pct_UltMes].CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 814_Insert_AfiliadoImponibleFranja =====
-- DependsOn: 814_AfiliadoImponibleFranja
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_814_Insert_AfiliadoImponibleFranja]
    @pMes INT,
    @pSMN NVARCHAR(MAX),
    @pCodEmpresa INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_<125_Pct] ( Grupo, CI, Importe, Nombre )
    SELECT [814_AfiliadoImponibleFranja].Grupo, [814_AfiliadoImponibleFranja].CI, [814_AfiliadoImponibleFranja].Importe, [Afiliado].[Nombres] + ' ' + [Afiliado].[Apellido1] + (CASE WHEN [Afiliado].[Apellido2] + ''<>'' THEN ' ' + [Afiliado].[Apellido2] ELSE '' END) AS DescAfiliado
    FROM [814_AfiliadoImponibleFranja](@pMes, @pSMN, @pCodEmpresa) INNER JOIN Afiliado ON [814_AfiliadoImponibleFranja].CI = Afiliado.CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 813_Insert_CertificadosAfeccion =====
-- DependsOn: 813_CertificadosAfeccion
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_813_Insert_CertificadosAfeccion]
    @pCodPatologia NVARCHAR(MAX),
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Codigo, Descrip, Cantidad )
    SELECT [813_CertificadosAfeccion].Codigo, [813_CertificadosAfeccion].Descrip, [813_CertificadosAfeccion].Cantidad
    FROM [813_CertificadosAfeccion](NULL, NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 806_Insert_Certificados_Cantidad =====
-- DependsOn: 806_CertificadosCantidad
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_806_Insert_Certificados_Cantidad]
    @pFechaIni DATETIME2(0),
    @pFechaFin DATETIME2(0)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Cantidad, Descrip )
    SELECT [806_CertificadosCantidad].Cantidad, [806_CertificadosCantidad].GE
    FROM [806_CertificadosCantidad](NULL, NULL);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 810_Insert_AfiliadoActivoCantidad_GE =====
-- DependsOn: 810_AfiliadosActivos_GE
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_810_Insert_AfiliadoActivoCantidad_GE]
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip] ( Descrip, Cantidad )
    SELECT [810_AfiliadosActivos_GE].DescGrupoEtario, Count(*) AS Cantidad
    FROM [810_AfiliadosActivos_GE](NULL)
    GROUP BY [810_AfiliadosActivos_GE].DescGrupoEtario;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 827_Insert_AfiliadosActivos_GE_Sexo =====
-- DependsOn: 827_AfiliadosActivos_GE_Sexo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_827_Insert_AfiliadosActivos_GE_Sexo]
    @pFecha NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_CantidadDescrip2] ( Descrip, Descrip2, Cantidad )
    SELECT [827_AfiliadosActivos_GE_Sexo].Sexo, [827_AfiliadosActivos_GE_Sexo].DescGrupoEtario, Count(*) AS Expr1
    FROM [827_AfiliadosActivos_GE_Sexo](NULL)
    GROUP BY [827_AfiliadosActivos_GE_Sexo].Sexo, [827_AfiliadosActivos_GE_Sexo].DescGrupoEtario;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 250_AfiliadoaControlar =====
-- DependsOn: 250_AfiliadoConDerecho
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_250_AfiliadoaControlar]
    @pMesFin INT,
    @pMesIni INT,
    @pSMN INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CI, Trabaja.CodEmpresa
    FROM ((([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
    WHERE (Trabaja.FechaBaja Is Null) AND Mutualista.Ficticia= 0 AND Afiliado.CodSituacionMutual='AF' AND Empresa.Ficticia = 0;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: zConsulta2 =====
-- DependsOn: 250_AfiliadoConDerecho
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_zConsulta2]
    @pMesFin INT,
    @pMesIni INT,
    @pSMN INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Trabaja.CI, Trabaja.CodEmpresa
    FROM ((([acc_sgpa_250_AfiliadoConDerecho_q](@pMesFin, @pMesIni, @pSMN) AS [250_AfiliadoConDerecho] INNER JOIN Trabaja ON [250_AfiliadoConDerecho].ci = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Afiliado ON [250_AfiliadoConDerecho].ci = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
    WHERE (Trabaja.FechaBaja Is Null) AND Mutualista.Ficticia= 0 AND Afiliado.CodSituacionMutual='AF' AND Empresa.Ficticia = 0;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Insert_Afiliado_Certificado =====
-- DependsOn: 480_Certificacion, 480_Prorrogas, 480_Promedio
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Insert_Afiliado_Certificado]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Afiliado_Certificado] ( CI, Nombres, Apellido1, Apellido2, FechaNacimiento, Sexo, CodMutualista, DescMutualista, Promedio, F_Ult_Prorroga, DiaProrroga, DiasUltPro )
    SELECT Afiliado.CI, MIN(Afiliado.Nombres) AS FirstOfNombres, MIN(Afiliado.Apellido1) AS FirstOfApellido1, MIN(Afiliado.Apellido2) AS FirstOfApellido2, MIN(Afiliado.FechaNacimiento) AS FirstOfFechaNacimiento, MIN(Afiliado.Sexo) AS FirstOfSexo, MIN(Afiliado.CodMutualista) AS FirstOfCodMutualista, MIN(Mutualista.Descrip) AS FirstOfNombre, MIN(acc_sgpa_480_Promedio_q.Promedio) AS FirstOfPromedio, MIN(acc_sgpa_480_Prorrogas_q.F_Ult_Prorroga) AS FirstOfF_Ult_Prorroga, MIN(acc_sgpa_480_Prorrogas_q.Dias) AS FirstOfDias, acc_sgpa_480_Prorrogas_q.DiasUltPro
    FROM ((([acc_sgpa_480_Certificacion_q] AS acc_sgpa_480_Certificacion_q INNER JOIN Afiliado ON acc_sgpa_480_Certificacion_q.CI = Afiliado.CI) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista) LEFT JOIN [acc_sgpa_480_Prorrogas_q] ON Afiliado.CI = acc_sgpa_480_Prorrogas_q.CI) LEFT JOIN [acc_sgpa_480_Promedio_q] ON Afiliado.CI = acc_sgpa_480_Promedio_q.CI
    GROUP BY Afiliado.CI, acc_sgpa_480_Prorrogas_q.DiasUltPro;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 480_Insert_Certificacion_Afeccion =====
-- DependsOn: 480_Certificacion
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_480_Insert_Certificacion_Afeccion]
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Afiliado_Certificado_Afeccion] ( CI, CodAfeccionTipo, DescAfeccionTipo, Cantidad, Dias )
    SELECT acc_sgpa_480_Certificacion_q.CI, acc_sgpa_480_Certificacion_q.CodAfeccionTipo, MIN(AfeccionTipo.Descrip) AS FirstOfDescrip, Count(*) AS Cantidad, Sum(DATEDIFF(day,acc_sgpa_480_Certificacion_q.[FechaIni],acc_sgpa_480_Certificacion_q.[FechaFin])+1) AS SumOfCI
    FROM [acc_sgpa_480_Certificacion_q] INNER JOIN AfeccionTipo ON acc_sgpa_480_Certificacion_q.CodAfeccionTipo = AfeccionTipo.CodAfeccionTipo
    WHERE (((acc_sgpa_480_Certificacion_q.Efectiva)= 1))
    GROUP BY acc_sgpa_480_Certificacion_q.CI, acc_sgpa_480_Certificacion_q.CodAfeccionTipo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 300_SubsidioItemCod_Full =====
-- DependsOn: acc_sgpa_300_SubsidioItemCod_Full_Data_q
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_300_SubsidioItemCod_Full]
    @pFecha DATETIME2(0),
    @pCI INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [acc_sgpa_300_SubsidioItemCod_Full_Data_q].*
    FROM [acc_sgpa_300_SubsidioItemCod_Full_Data_q](@pFecha, @pCI)
    ORDER BY [acc_sgpa_300_SubsidioItemCod_Full_Data_q].ModificaNominal, [acc_sgpa_300_SubsidioItemCod_Full_Data_q].CodSubsidioItemCod;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_CertificadoEmpresa =====
-- DependsOn: 500_Rpt_CertificadoEmpresa_S
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_CertificadoEmpresa]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.NroLlamado, (CASE WHEN LEN(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI])>=8 THEN FORMAT(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI],'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI],'@@@\.@@@-@') END) AS CI, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.NroRecibo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaRecibido, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaCertificacion, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaIni, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.FechaFin, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodAfeccionTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescAfeccionTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodCertificador, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescCertificador, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodSalidaTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescSalidaTipo, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Efectiva, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Indicaciones, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.ImporteDeducible, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Usr, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Ts, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Nombres, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Apellido1, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.Apellido2, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescAfiliado, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.CodEmpresa, acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.DescEmpresa
    FROM [acc_sgpa_500_Rpt_CertificadoEmpresa_S_q]
    WHERE (acc_sgpa_500_Rpt_CertificadoEmpresa_S_q.[CI] = 13606922)
    ORDER BY [NroLlamado];
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: xRptEmpresaPromedioNo0 =====
-- DependsOn: xEmpresaPromedioNo0
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_xRptEmpresaPromedioNo0]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Empresa.Nombre, acc_sgpa_xEmpresaPromedioNo0_q.PromedioDeImporte AS Promedio
    FROM [acc_sgpa_xEmpresaPromedioNo0_q] INNER JOIN Empresa ON acc_sgpa_xEmpresaPromedioNo0_q.CodEmpresa = Empresa.CodEmpresa;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 200_Insert_600_Rpt_AfiliadoMutualista =====
-- DependsOn: 200_PagarMutualista
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_200_Insert_600_Rpt_AfiliadoMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_AfiliadoMutualista] ( CI, DescAfiliado, CodMutualista, DescMutualista, Cuota, Sexo, FechaNacimiento, DescSituacionMutual )
    SELECT acc_sgpa_200_PagarMutualista_q.CI, acc_sgpa_200_PagarMutualista_q.DescAfiliado, acc_sgpa_200_PagarMutualista_q.CodMutualista, Mutualista.Descrip, Mutualista.Cuota, Afiliado.Sexo, Afiliado.FechaNacimiento, SituacionMutual.Descrip
    FROM (([acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS acc_sgpa_200_PagarMutualista_q INNER JOIN Mutualista ON acc_sgpa_200_PagarMutualista_q.CodMutualista = Mutualista.CodMutualista) INNER JOIN Afiliado ON acc_sgpa_200_PagarMutualista_q.CI = Afiliado.CI) LEFT JOIN SituacionMutual ON acc_sgpa_200_PagarMutualista_q.CodSituacionMutual = SituacionMutual.CodSituacionMutual;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: zConsulta1 =====
-- DependsOn: 200_PagarMutualista
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_zConsulta1]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT acc_sgpa_200_PagarMutualista_q.CI, Trabaja.CodEmpresa
    FROM (([acc_sgpa_200_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS acc_sgpa_200_PagarMutualista_q INNER JOIN Afiliado ON acc_sgpa_200_PagarMutualista_q.CI = Afiliado.CI) INNER JOIN Trabaja ON Afiliado.CI = Trabaja.CI) INNER JOIN Empresa ON Trabaja.CodEmpresa = Empresa.CodEmpresa
    WHERE (((Afiliado.CodSituacionMutual)='AF') AND ((Afiliado.CodMutualista) NOT In (0,38,40)) AND ((Trabaja.FechaBaja) Is Null));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rpt_NoPagarMutualista =====
-- DependsOn: 201_PagarMutualista, Rs_TrabajaActivo
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_Rpt_NoPagarMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Afiliado.*, Afiliado.Nombres + ' ' + Afiliado.Apellido1 + (CASE WHEN Afiliado.Apellido2 + ''<>'' THEN ' ' + Afiliado.Apellido2 ELSE '' END) AS DescAfiliado, acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa, Empresa.Nombre AS DescEmpresa, Mutualista.Descrip AS DescMutualista
    FROM (((Afiliado LEFT JOIN [acc_sgpa_201_PagarMutualista_q](@pMes, @pMesIni, @pSMN) AS [201_PagarMutualista] ON Afiliado.CI = [201_PagarMutualista].CI) INNER JOIN [acc_sgpa_Rs_TrabajaActivo_q] ON Afiliado.CI = acc_sgpa_Rs_TrabajaActivo_q.CI) INNER JOIN Empresa ON acc_sgpa_Rs_TrabajaActivo_q.CodEmpresa = Empresa.CodEmpresa) INNER JOIN Mutualista ON Afiliado.CodMutualista = Mutualista.CodMutualista
    WHERE ((([201_PagarMutualista].CI) Is Null) AND ((Afiliado.CodMutualista)>0))
    ORDER BY [201_PagarMutualista].CI;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 500_Rpt_NoPagarMutualista =====
-- DependsOn: Rpt_NoPagarMutualista
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_500_Rpt_NoPagarMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT (CASE WHEN LEN(acc_sgpa_Rpt_NoPagarMutualista_q.[CI])>=8 THEN FORMAT(acc_sgpa_Rpt_NoPagarMutualista_q.[CI],'@\.@@@\.@@@-@') ELSE FORMAT(acc_sgpa_Rpt_NoPagarMutualista_q.[CI],'@@@\.@@@-@') END) AS CI, acc_sgpa_Rpt_NoPagarMutualista_q.Nombres, acc_sgpa_Rpt_NoPagarMutualista_q.Apellido1, acc_sgpa_Rpt_NoPagarMutualista_q.Apellido2, acc_sgpa_Rpt_NoPagarMutualista_q.FechaNacimiento, acc_sgpa_Rpt_NoPagarMutualista_q.Sexo, acc_sgpa_Rpt_NoPagarMutualista_q.Direccion, acc_sgpa_Rpt_NoPagarMutualista_q.Telefono, acc_sgpa_Rpt_NoPagarMutualista_q.EMail, acc_sgpa_Rpt_NoPagarMutualista_q.CodMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.FechaIngMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.NroSocioMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.CodRegimenJubilatorio, acc_sgpa_Rpt_NoPagarMutualista_q.CodDepartamento, acc_sgpa_Rpt_NoPagarMutualista_q.PagaMutualista, acc_sgpa_Rpt_NoPagarMutualista_q.Usr, acc_sgpa_Rpt_NoPagarMutualista_q.Ts, acc_sgpa_Rpt_NoPagarMutualista_q.DescAfiliado, acc_sgpa_Rpt_NoPagarMutualista_q.CodEmpresa, acc_sgpa_Rpt_NoPagarMutualista_q.DescEmpresa, acc_sgpa_Rpt_NoPagarMutualista_q.DescMutualista
    FROM [acc_sgpa_Rpt_NoPagarMutualista_q](@pMes, @pMesIni, @pSMN);
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: 201_Insert_Rpt_AfiliadoMutualista =====
-- DependsOn: 500_Rpt_NoPagarMutualista
CREATE OR ALTER PROCEDURE [dbo].[acc_sgpa_201_Insert_Rpt_AfiliadoMutualista]
    @pMes INT,
    @pMesIni INT,
    @pSMN NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [600_Rpt_AfiliadoMutualista] ( CI, Nombres, Apellido1, Apellido2, FechaNacimiento, Sexo, Direccion, Telefono, EMail, CodMutualista, DescMutualista, FechaIngMutualista, NroSocioMutualista, CodRegimenJubilatorio, CodDepartamento, PagaMutualista, Usr, Ts, DescAfiliado, CodEmpresa, DescEmpresa )
    SELECT [500_Rpt_NoPagarMutualista].CI, [500_Rpt_NoPagarMutualista].Nombres, [500_Rpt_NoPagarMutualista].Apellido1, [500_Rpt_NoPagarMutualista].Apellido2, [500_Rpt_NoPagarMutualista].FechaNacimiento, [500_Rpt_NoPagarMutualista].Sexo, [500_Rpt_NoPagarMutualista].Direccion, [500_Rpt_NoPagarMutualista].Telefono, [500_Rpt_NoPagarMutualista].EMail, [500_Rpt_NoPagarMutualista].CodMutualista, [500_Rpt_NoPagarMutualista].DescMutualista, [500_Rpt_NoPagarMutualista].FechaIngMutualista, [500_Rpt_NoPagarMutualista].NroSocioMutualista, [500_Rpt_NoPagarMutualista].CodRegimenJubilatorio, [500_Rpt_NoPagarMutualista].CodDepartamento, [500_Rpt_NoPagarMutualista].PagaMutualista, [500_Rpt_NoPagarMutualista].Usr, [500_Rpt_NoPagarMutualista].Ts, [500_Rpt_NoPagarMutualista].DescAfiliado, [500_Rpt_NoPagarMutualista].CodEmpresa, [500_Rpt_NoPagarMutualista].DescEmpresa
    FROM [500_Rpt_NoPagarMutualista](@pMes, @pMesIni, @pSMN);
END;

GO

